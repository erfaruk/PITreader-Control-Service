using Microsoft.EntityFrameworkCore;
using ReaderWorker.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Net.Http.Headers;
using ReaderWorker.Models.API;


namespace ReaderWorker.Services
{
    public class ReaderStatus
    {

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
            // HTTP isteği için Authorization başlığı ekle
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "Gz5S2QrZnG1/neyHj6c4gg==");
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

        public async Task<AuthenticationStatusResponse> StatusAuth(string ip, int port, string token)
        {
            //sha certificate disable
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };
            var httpClient = new HttpClient(handler);
            httpClient.Timeout = TimeSpan.FromSeconds(10);
            string Url = $"https://{ip}:{port}/api/status/authentication";
            // HTTP isteği için Authorization başlığı ekle
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"{token}");
            //Get data response
            try
            {
                string response = await httpClient.GetStringAsync(Url);
                //string response = "{\"authenticated\":true,\"permission\":116275,\"authenticationStatus\":1,\"failureReason\":0,\"orderNo\":402264,\"salesVersion\":\"\",\"serialNo\":\"040408109\",\"transponderUid\":\"041B15C2CF4F80\",\"userData\":[{\"id\":10000,\"stringValue\":\"\"}]}";
                AuthenticationStatusResponse? statusResponse = JsonSerializer.Deserialize<AuthenticationStatusResponse>(response);
                return statusResponse;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
