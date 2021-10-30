using CRM.Desktop.Data.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Desktop.Data.Helpers
{
    public class WebApiClient
    {
        private readonly HttpClient webClient;
        private readonly JsonSerializer serializer;
        private const string BASE_ADDRESS = "https://localhost:44395/api/";

        public WebApiClient()
        {
            webClient = new HttpClient();
            webClient.BaseAddress = new Uri(BASE_ADDRESS);
            webClient.DefaultRequestHeaders.Add("accept", "*/*");
            serializer = new JsonSerializer();
        }

        public WebApiClient(string token)
        {
            webClient = new HttpClient();
            webClient.BaseAddress = new Uri(BASE_ADDRESS);
            webClient.DefaultRequestHeaders.Add("accept", "*/*");
            webClient.DefaultRequestHeaders.Add("Authorization", "Bearer "+token);
            serializer = new JsonSerializer();
        }

        public async Task<ResponseModel<LoginResponse>> LoginAsync(LoginDto dto)
        {
            try
            {
                HttpResponseMessage response = await webClient.PostAsync($"Account/Login",new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json"));

                response.EnsureSuccessStatusCode();
                var contentStream = await response.Content.ReadAsStreamAsync();

                var streamReader = new StreamReader(contentStream);
                var jsonReader = new JsonTextReader(streamReader);

                return serializer.Deserialize<ResponseModel<LoginResponse>>(jsonReader);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ResponseModel<AdministrationDataDto>> GetAdministrationDataAsync()
        {
            try
            {
                HttpResponseMessage response = await webClient.GetAsync($"Administration/GetCompanyData");
                response.EnsureSuccessStatusCode();
                var contentStream = await response.Content.ReadAsStreamAsync();

                var streamReader = new StreamReader(contentStream);
                var jsonReader = new JsonTextReader(streamReader);

                return serializer.Deserialize<ResponseModel<AdministrationDataDto>>(jsonReader);
            }
            catch
            {
                return null;
            }
        }
    }
}
