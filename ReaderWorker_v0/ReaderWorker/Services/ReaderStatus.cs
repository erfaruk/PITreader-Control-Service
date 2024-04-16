using Microsoft.EntityFrameworkCore;
using ReaderWorker.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Pilz.PITreader.Client.Model;


namespace ReaderWorker.Services
{
    public class ReaderStatus
    {
        public List<Pitreader> AccesControl()
        {
            try {
                using var context = new ReaderExpertContext();
                var readers = context.Pitreaders.ToList();
                if (readers != null )
                {
                    return readers;
                }
                else
                {
                    return new List<Pitreader>();
                }
            }
            catch {
                throw;
            }
            
        }

        //private static readonly HttpClient client = new HttpClient();
        public  async Task<StatusResponse> Status(string ip)
        {   
            //sha certificate disable
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };
            var httpClient = new HttpClient(handler);
            httpClient.Timeout = TimeSpan.FromSeconds(1);
            string Url = $"https://{ip}/api/status/";
            //Get data response
            try
            {
                string response = await httpClient.GetStringAsync(Url);
                StatusResponse? statusResponse = JsonSerializer.Deserialize<StatusResponse>(response);
                return statusResponse;
            }
            catch(Exception ex) 
            {
                return null;
            } 
        }
    }
}
