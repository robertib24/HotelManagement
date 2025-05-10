using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using HotelManagement.Client.Models;
using Newtonsoft.Json;

namespace HotelManagement.Client.Services
{
    public class ServiceService
    {
        public async Task<List<ServiceModel>> GetAllServicesAsync()
        {
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync("services"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<ServiceModel>>(result);
                }
                else
                {
                    throw new Exception($"Error: {response.ReasonPhrase}");
                }
            }
        }

        public async Task<ServiceModel> GetServiceByIdAsync(int id)
        {
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync($"services/{id}"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<ServiceModel>(result);
                }
                else
                {
                    throw new Exception($"Error: {response.ReasonPhrase}");
                }
            }
        }

        public async Task<List<ServiceModel>> GetServicesByCategoryAsync(string category)
        {
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync($"services/category/{category}"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<ServiceModel>>(result);
                }
                else
                {
                    throw new Exception($"Error: {response.ReasonPhrase}");
                }
            }
        }

        public async Task<List<ServiceModel>> GetAvailableServicesAsync()
        {
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync("services/available"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<ServiceModel>>(result);
                }
                else
                {
                    throw new Exception($"Error: {response.ReasonPhrase}");
                }
            }
        }

        public async Task<ServiceModel> CreateServiceAsync(ServiceModel service)
        {
            var content = new StringContent(JsonConvert.SerializeObject(service), System.Text.Encoding.UTF8, "application/json");

            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync("services", content))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<ServiceModel>(result);
                }
                else
                {
                    throw new Exception($"Error: {response.ReasonPhrase}");
                }
            }
        }

        public async Task UpdateServiceAsync(ServiceModel service)
        {
            var content = new StringContent(JsonConvert.SerializeObject(service), System.Text.Encoding.UTF8, "application/json");

            using (HttpResponseMessage response = await ApiHelper.ApiClient.PutAsync($"services/{service.Id}", content))
            {
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Error: {response.ReasonPhrase}");
                }
            }
        }

        public async Task DeleteServiceAsync(int id)
        {
            using (HttpResponseMessage response = await ApiHelper.ApiClient.DeleteAsync($"services/{id}"))
            {
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Error: {response.ReasonPhrase}");
                }
            }
        }
    }
}