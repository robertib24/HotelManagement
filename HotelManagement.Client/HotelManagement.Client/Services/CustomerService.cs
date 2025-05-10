using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using HotelManagement.Client.Models;
using Newtonsoft.Json;

namespace HotelManagement.Client.Services
{
    public class CustomerService
    {
        public async Task<List<CustomerModel>> GetAllCustomersAsync()
        {
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync("customers"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<CustomerModel>>(result);
                }
                else
                {
                    throw new Exception($"Error: {response.ReasonPhrase}");
                }
            }
        }

        public async Task<CustomerModel> GetCustomerByIdAsync(int id)
        {
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync($"customers/{id}"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<CustomerModel>(result);
                }
                else
                {
                    throw new Exception($"Error: {response.ReasonPhrase}");
                }
            }
        }

        public async Task<List<CustomerModel>> GetVIPCustomersAsync()
        {
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync("customers/vip"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<CustomerModel>>(result);
                }
                else
                {
                    throw new Exception($"Error: {response.ReasonPhrase}");
                }
            }
        }

        public async Task<CustomerModel> GetCustomerByEmailAsync(string email)
        {
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync($"customers/email/{email}"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<CustomerModel>(result);
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return null;
                }
                else
                {
                    throw new Exception($"Error: {response.ReasonPhrase}");
                }
            }
        }

        public async Task<List<ReservationModel>> GetCustomerReservationsAsync(int customerId)
        {
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync($"customers/{customerId}/reservations"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<ReservationModel>>(result);
                }
                else
                {
                    throw new Exception($"Error: {response.ReasonPhrase}");
                }
            }
        }

        public async Task<CustomerModel> CreateCustomerAsync(CustomerModel customer)
        {
            var content = new StringContent(JsonConvert.SerializeObject(customer), System.Text.Encoding.UTF8, "application/json");

            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync("customers", content))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<CustomerModel>(result);
                }
                else
                {
                    string errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error: {response.ReasonPhrase}, Details: {errorContent}");
                }
            }
        }

        public async Task UpdateCustomerAsync(CustomerModel customer)
        {
            var content = new StringContent(JsonConvert.SerializeObject(customer), System.Text.Encoding.UTF8, "application/json");

            using (HttpResponseMessage response = await ApiHelper.ApiClient.PutAsync($"customers/{customer.Id}", content))
            {
                if (!response.IsSuccessStatusCode)
                {
                    string errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error: {response.ReasonPhrase}, Details: {errorContent}");
                }
            }
        }

        public async Task DeleteCustomerAsync(int id)
        {
            using (HttpResponseMessage response = await ApiHelper.ApiClient.DeleteAsync($"customers/{id}"))
            {
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Error: {response.ReasonPhrase}");
                }
            }
        }
    }
}