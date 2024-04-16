using Microsoft.Extensions.Logging;
using Pilz.PITreader.Client.Model;
using ReaderWorker.Models.Database;
using ReaderWorker.Services;

namespace ReaderWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly  ReaderStatus _readers;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
            _readers = new ReaderStatus();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    //_logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                   
                    List<Pitreader> readerList = _readers.AccesControl();
                    

                    readerList?.ForEach(PITReaders =>
                    {
                        var statusResponse = _readers.Status( PITReaders.Ipaddress).Result;
                        if (statusResponse != null)
                        {
                            _logger.LogInformation($"IP Address:{PITReaders.Ipaddress}\n " +
                                                   $"\tProdType:{statusResponse.ProductType}" +
                                                   $"\tHostName:{statusResponse.HostName}" +
                                                   $"\tMacAddress:{statusResponse.MacAddress}" +
                                                   $"\tAuthenticated:{statusResponse.TransponderAuthenticated}");
                }
                        else
                        {
                            _logger.LogInformation($"{PITReaders.Ipaddress} Cihazdan dönüþ alýnamadý");
                        }
                    }
                    );
                    
                }
                await Task.Delay(300, stoppingToken);
            }
        }
    }
}
