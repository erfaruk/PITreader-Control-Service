using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using ReaderWorker.Models.API;
using ReaderWorker.Models.Database;
using ReaderWorker.Services;

namespace ReaderWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly Readers _readers;
        private readonly  ReaderStatus _status;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
            _readers = new Readers();
            _status = new ReaderStatus();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    //_logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                    _logger.LogInformation("--------------------------------------------------------------------------------");
                    List <Pitreader> readerList = _readers.AccesControl();

                    readerList?.ForEach(PITReader =>
                    {
                        var statusResponse = _status.Status( PITReader.Ipaddress).Result;
                        var statusAuthResponse = _status.StatusAuth(PITReader.Ipaddress, PITReader.Port, PITReader.Apitoken).Result;
                        using var context = new ReaderExpertContext();
                        var entityToUpdate = context.Pitreaders.FirstOrDefault(e => e.ReaderId == PITReader.ReaderId);
                        if (statusResponse != null && statusAuthResponse!=null)
                        {
                            _logger.LogInformation($"IP Address:{PITReader.Ipaddress}\n " +
                                                   $"\tProdType:{statusResponse.ProductType}" +
                                                   $"\tHostName:{statusResponse.HostName}" +
                                                   $"\tMacAddress:{statusResponse.MacAddress}" +
                                                   $"\tAuthenticated:{statusResponse.TransponderAuthenticated}\n" +
                                                   $"\tAuthenticated:{statusAuthResponse.Authenticated}" +
                                                   $"\tPermission:{statusAuthResponse.Permission}" +
                                                   $"\tSecurityId:{statusAuthResponse.securityId}");

                            
                            if (entityToUpdate != null)
                            {
                                if (statusResponse.TransponderAuthenticated == true && entityToUpdate.IsKeyIn != true)
                                {
                                    entityToUpdate.Status = true;
                                    var keyId = context.Transponders.FirstOrDefault(e => e.SecurityId == statusAuthResponse.securityId);
                                    Permission permission = statusAuthResponse.Permission;
                                    int permissionVal = Convert.ToInt32(permission.ToString().Replace("Permission_",""));
                                    if (keyId != null)
                                    {
                                        entityToUpdate.KeyId = keyId.KeyId;
                                    }
                                    entityToUpdate.Permission = permissionVal;
                                    context.SaveChanges();
                                }
                                else if (statusResponse.TransponderAuthenticated == false && entityToUpdate.IsKeyIn != false)
                                {
                                    entityToUpdate.IsKeyIn = false;
                                    entityToUpdate.KeyId = null;
                                    entityToUpdate.Permission=null;
                                    context.SaveChanges();
                                }
                            }      
                        }
                        else
                        {
                            _logger.LogInformation($"{PITReader.Ipaddress} Cihazdan dönüþ alýnamadý");
                            if (entityToUpdate != null)
                            {
                                entityToUpdate.IsKeyIn = false;
                                entityToUpdate.Status = false;
                                entityToUpdate.KeyId = null;
                                entityToUpdate.Permission = null;
                                context.SaveChanges();
                            }
                                
                        }
                    }
                    );
                    
                }
                await Task.Delay(500, stoppingToken);
            }
        }
    }
}
