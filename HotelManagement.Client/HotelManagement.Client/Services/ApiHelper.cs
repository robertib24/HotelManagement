using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;

namespace HotelManagement.Client.Services
{
    public static class ApiHelper
    {
        public static HttpClient ApiClient { get; set; }

        public static void InitializeClient()
        {
            ApiClient = new HttpClient();
            ApiClient.BaseAddress = new Uri(ConfigurationManager.AppSettings["ApiBaseUrl"]);
            ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            ApiClient.Timeout = TimeSpan.FromSeconds(30);
        }
    }
}