using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using HotelManagement.Client.Models;
using Newtonsoft.Json;

namespace HotelManagement.Client.Services
{
    public class ReservationServiceService
    {
        public async Task<List<ReservationServiceModel>> GetServicesByReservationAsync(int reservationId)
        {
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync($"reservationservices/reservation/{reservationId}"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<ReservationServiceModel>>(result);
                }
                else
                {
                    throw new Exception($"Error: {response.ReasonPhrase}");
                }
            }
        }

        public async Task<decimal> GetTotalServicesCostAsync(int reservationId)
        {
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync($"reservationservices/reservation/{reservationId}/total"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<decimal>(result);
                }
                else
                {
                    throw new Exception($"Error: {response.ReasonPhrase}");
                }
            }
        }

        public async Task AddServiceToReservationAsync(int reservationId, int serviceId, int quantity, DateTime date, string notes)
        {
            var model = new
            {
                ReservationId = reservationId,
                ServiceId = serviceId,
                Quantity = quantity,
                Date = date,
                Notes = notes
            };

            var content = new StringContent(JsonConvert.SerializeObject(model), System.Text.Encoding.UTF8, "application/json");

            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync("reservationservices", content))
            {
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Error: {response.ReasonPhrase}");
                }
            }
        }

        public async Task RemoveServiceFromReservationAsync(int reservationServiceId)
        {
            using (HttpResponseMessage response = await ApiHelper.ApiClient.DeleteAsync($"reservationservices/{reservationServiceId}"))
            {
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Error: {response.ReasonPhrase}");
                }
            }
        }
    }

    public class ReservationServiceModel
    {
        public int Id { get; set; }
        public int ReservationId { get; set; }
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public string ServiceCategory { get; set; }
        public DateTime Date { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public string Notes { get; set; }
    }
}