using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using HotelManagement.Client.Models;
using Newtonsoft.Json;

namespace HotelManagement.Client.Services
{
    public class InvoiceService
    {
        public async Task<List<InvoiceModel>> GetAllInvoicesAsync()
        {
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync("invoices"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<InvoiceModel>>(result);
                }
                else
                {
                    throw new Exception($"Error: {response.ReasonPhrase}");
                }
            }
        }

        public async Task<InvoiceModel> GetInvoiceByIdAsync(int id)
        {
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync($"invoices/{id}"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<InvoiceModel>(result);
                }
                else
                {
                    throw new Exception($"Error: {response.ReasonPhrase}");
                }
            }
        }

        public async Task<InvoiceModel> GetInvoiceByReservationAsync(int reservationId)
        {
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync($"invoices/reservation/{reservationId}"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<InvoiceModel>(result);
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

        public async Task<InvoiceModel> GenerateInvoiceAsync(int reservationId)
        {
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync($"invoices/generate/{reservationId}", null))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<InvoiceModel>(result);
                }
                else
                {
                    throw new Exception($"Error: {response.ReasonPhrase}");
                }
            }
        }

        public async Task<List<InvoiceModel>> GetUnpaidInvoicesAsync()
        {
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync("invoices/unpaid"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<InvoiceModel>>(result);
                }
                else
                {
                    throw new Exception($"Error: {response.ReasonPhrase}");
                }
            }
        }

        public async Task<List<InvoiceModel>> GetPaidInvoicesAsync()
        {
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync("invoices/paid"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<InvoiceModel>>(result);
                }
                else
                {
                    throw new Exception($"Error: {response.ReasonPhrase}");
                }
            }
        }

        public async Task MarkAsPaidAsync(int invoiceId, string paymentMethod)
        {
            var content = new StringContent(JsonConvert.SerializeObject(new { PaymentMethod = paymentMethod }), System.Text.Encoding.UTF8, "application/json");

            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync($"invoices/{invoiceId}/markaspaid", content))
            {
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Error: {response.ReasonPhrase}");
                }
            }
        }
    }
}