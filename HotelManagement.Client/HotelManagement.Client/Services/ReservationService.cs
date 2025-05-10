using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using HotelManagement.Client.Models;
using Newtonsoft.Json;

namespace HotelManagement.Client.Services
{
    public class ReservationService
    {
        public async Task<List<ReservationModel>> GetAllReservationsAsync()
        {
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync("reservations"))
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

        public async Task<ReservationModel> GetReservationByIdAsync(int id)
        {
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync($"reservations/{id}"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<ReservationModel>(result);
                }
                else
                {
                    throw new Exception($"Error: {response.ReasonPhrase}");
                }
            }
        }

        public async Task<List<ReservationModel>> GetReservationsByCustomerAsync(int customerId)
        {
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync($"reservations/customer/{customerId}"))
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

        public async Task<List<ReservationModel>> GetReservationsByHotelAsync(int hotelId)
        {
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync($"reservations/hotel/{hotelId}"))
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

        public async Task<List<ReservationModel>> GetActiveReservationsAsync()
        {
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync("reservations/active"))
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

        public async Task<List<ReservationModel>> GetCheckInsForTodayAsync()
        {
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync("reservations/checkins-today"))
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

        public async Task<List<ReservationModel>> GetCheckOutsForTodayAsync()
        {
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync("reservations/checkouts-today"))
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

        public async Task<ReservationModel> CreateReservationAsync(ReservationModel reservation)
        {
            var content = new StringContent(JsonConvert.SerializeObject(reservation), System.Text.Encoding.UTF8, "application/json");

            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync("reservations", content))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<ReservationModel>(result);
                }
                else
                {
                    string errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error: {response.ReasonPhrase}, Details: {errorContent}");
                }
            }
        }

        public async Task UpdateReservationAsync(ReservationModel reservation)
        {
            var content = new StringContent(JsonConvert.SerializeObject(reservation), System.Text.Encoding.UTF8, "application/json");

            using (HttpResponseMessage response = await ApiHelper.ApiClient.PutAsync($"reservations/{reservation.Id}", content))
            {
                if (!response.IsSuccessStatusCode)
                {
                    string errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error: {response.ReasonPhrase}, Details: {errorContent}");
                }
            }
        }

        public async Task CheckInAsync(int reservationId)
        {
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync($"reservations/{reservationId}/check-in", null))
            {
                if (!response.IsSuccessStatusCode)
                {
                    string errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error: {response.ReasonPhrase}, Details: {errorContent}");
                }
            }
        }

        public async Task CheckOutAsync(int reservationId)
        {
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync($"reservations/{reservationId}/check-out", null))
            {
                if (!response.IsSuccessStatusCode)
                {
                    string errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error: {response.ReasonPhrase}, Details: {errorContent}");
                }
            }
        }

        public async Task CancelReservationAsync(int reservationId)
        {
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync($"reservations/{reservationId}/cancel", null))
            {
                if (!response.IsSuccessStatusCode)
                {
                    string errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error: {response.ReasonPhrase}, Details: {errorContent}");
                }
            }
        }

        public async Task DeleteReservationAsync(int id)
        {
            using (HttpResponseMessage response = await ApiHelper.ApiClient.DeleteAsync($"reservations/{id}"))
            {
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Error: {response.ReasonPhrase}");
                }
            }
        }
    }
}