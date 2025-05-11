using System;
using System.Collections.Generic;
using System.Linq;
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
            try
            {
                var requestObj = new
                {
                    CustomerId = reservation.CustomerId,
                    RoomId = reservation.RoomId,
                    CheckInDate = reservation.CheckInDate.ToString("yyyy-MM-ddTHH:mm:ss"),
                    CheckOutDate = reservation.CheckOutDate.ToString("yyyy-MM-ddTHH:mm:ss"),
                    NumberOfGuests = reservation.NumberOfGuests,
                    Notes = reservation.Notes,
                    ReservationStatus = "Confirmed"
                };

                var jsonContent = JsonConvert.SerializeObject(requestObj);
                var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");

                System.Diagnostics.Debug.WriteLine($"Sending reservation request: {jsonContent}");

                using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync("reservations", content))
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    System.Diagnostics.Debug.WriteLine($"Response code: {(int)response.StatusCode} {response.ReasonPhrase}");
                    System.Diagnostics.Debug.WriteLine($"Response content: {responseContent}");

                    var allReservations = await GetAllReservationsAsync();
                    if (allReservations != null && allReservations.Count > 0)
                    {
                        var latestReservation = allReservations
                            .OrderByDescending(r => r.Id)
                            .FirstOrDefault();

                        if (latestReservation != null)
                        {
                            return latestReservation;
                        }
                    }

                    return reservation;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Exception in CreateReservationAsync: {ex.Message}");

                try
                {
                    var allReservations = await GetAllReservationsAsync();
                    if (allReservations != null && allReservations.Count > 0)
                    {
                        var matchingReservation = allReservations
                            .OrderByDescending(r => r.Id)
                            .FirstOrDefault(r =>
                                r.CustomerId == reservation.CustomerId &&
                                r.RoomId == reservation.RoomId &&
                                r.CheckInDate.Date == reservation.CheckInDate.Date &&
                                r.CheckOutDate.Date == reservation.CheckOutDate.Date);

                        if (matchingReservation != null)
                        {
                            return matchingReservation;
                        }
                    }
                }
                catch (Exception innerEx)
                {
                    System.Diagnostics.Debug.WriteLine($"Inner exception when trying to find saved reservation: {innerEx.Message}");
                }

                throw;
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