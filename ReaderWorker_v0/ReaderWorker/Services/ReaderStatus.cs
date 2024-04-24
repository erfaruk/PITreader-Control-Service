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
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;


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
            // TLS Session Tickets desteği için HttpClient yapılandırması
            handler.UseDefaultCredentials = true; // TLS Session Tickets'ı destekleyen bir REST istemcisi kullanılacaksa, varsayılan kimlik doğrulama kullanılır.
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
            var handler = new HttpClientHandler();
            // Sertifika doğrulamasını devre dışı bırakma
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
            var httpClient = new HttpClient(handler);
            httpClient.Timeout = TimeSpan.FromSeconds(2);
            // TLS Session Tickets desteği için HttpClient yapılandırması
            handler.UseDefaultCredentials = true; // TLS Session Tickets'ı destekleyen bir REST istemcisi kullanılacaksa, varsayılan kimlik doğrulama kullanılır.
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
        // SSL sertifikasını doğrulama fonksiyonu
        static bool ValidateCertificate(HttpRequestMessage request, X509Certificate2 certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            // Örnek doğrulama: Herhangi bir sertifikayı kabul et
            return true;
        }
    }
}




///////////////////////
/////sha certificate disable
//var handler = new HttpClientHandler();
// SSL sertifikasını doğrulamak için gerekirse ayarlar yapılabilir
//handler.ServerCertificateCustomValidationCallback = ValidateCertificate;
// Sertifika doğrulamasını devre dışı bırakma
//handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
//{
//    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
//};


// Sertifika yükleme
////X509Certificate2 certificate = new X509Certificate2(certificatePath);
////handler.ClientCertificates.Add(certificate);
///
// Sertifika dosyasının yolu
////string certificateFileName = "sertifika.cer";
// Sertifika dosyasının tam yolunu alın
////string certificatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Certificates", certificateFileName);
//sha certificate disable