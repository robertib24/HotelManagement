using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using HotelManagement.Client.Models;
using Newtonsoft.Json;

namespace HotelManagement.Client.Services
{
    public class RoomService
    {
        public async Task<List<RoomModel>> GetAllRoomsAsync()
        {
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync("rooms"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<RoomModel>>(result);
                }
                else
                {
                    throw new Exception($"Error: {response.ReasonPhrase}");
                }
            }
        }

        public async Task<RoomModel> GetRoomByIdAsync(int id)
        {
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync($"rooms/{id}"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<RoomModel>(result);
                }
                else
                {
                    throw new Exception($"Error: {response.ReasonPhrase}");
                }
            }
        }

        public async Task<List<RoomModel>> GetAvailableRoomsByHotelAsync(int hotelId, int? roomTypeId, DateTime checkIn, DateTime checkOut)
        {
            try
            {
                // Format the dates properly in the query string
                string relativeUrl = $"rooms/available?hotelId={hotelId}&checkIn={checkIn:yyyy-MM-dd}&checkOut={checkOut:yyyy-MM-dd}";

                if (roomTypeId.HasValue)
                {
                    relativeUrl += $"&roomTypeId={roomTypeId.Value}";
                }

                var response = await ApiHelper.ApiClient.GetAsync(relativeUrl);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"API Error: {response.StatusCode}\nDetails: {errorContent}");
                }

                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<RoomModel>>(content) ?? new List<RoomModel>();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Serious error getting rooms: {ex.Message}",
                              "API Error",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error);
                return new List<RoomModel>();
            }
        }

        public async Task<bool> IsRoomAvailableAsync(int roomId, DateTime checkIn, DateTime checkOut)
        {
            string endpoint = $"rooms/{roomId}/isAvailable?checkIn={checkIn:yyyy-MM-dd}&checkOut={checkOut:yyyy-MM-dd}";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(endpoint))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<bool>(result);
                }
                else
                {
                    throw new Exception($"Error: {response.ReasonPhrase}");
                }
            }
        }

        public async Task<RoomModel> CreateRoomAsync(RoomModel room)
        {
            var content = new StringContent(JsonConvert.SerializeObject(room), System.Text.Encoding.UTF8, "application/json");

            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync("rooms", content))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<RoomModel>(result);
                }
                else
                {
                    throw new Exception($"Error: {response.ReasonPhrase}");
                }
            }
        }

        public async Task UpdateRoomAsync(RoomModel room)
        {
            var content = new StringContent(JsonConvert.SerializeObject(room), System.Text.Encoding.UTF8, "application/json");

            using (HttpResponseMessage response = await ApiHelper.ApiClient.PutAsync($"rooms/{room.Id}", content))
            {
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Error: {response.ReasonPhrase}");
                }
            }
        }

        public async Task DeleteRoomAsync(int id)
        {
            using (HttpResponseMessage response = await ApiHelper.ApiClient.DeleteAsync($"rooms/{id}"))
            {
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Error: {response.ReasonPhrase}");
                }
            }
        }

        public async Task<List<RoomModel>> GetRoomsByHotelAsync(int hotelId)
        {
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync($"rooms/byHotel/{hotelId}"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<RoomModel>>(result);
                }
                else
                {
                    throw new Exception($"Error: {response.ReasonPhrase}");
                }
            }
        }

        public async Task UpdateRoomStatusAsync(int roomId, bool isOccupied, bool isClean, bool needsRepair)
        {
            var model = new
            {
                IsOccupied = isOccupied,
                IsClean = isClean,
                NeedsRepair = needsRepair
            };

            var content = new StringContent(JsonConvert.SerializeObject(model), System.Text.Encoding.UTF8, "application/json");

            using (HttpResponseMessage response = await ApiHelper.ApiClient.PatchAsync($"rooms/{roomId}/status", content))
            {
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Error: {response.ReasonPhrase}");
                }
            }
        }
    }
}