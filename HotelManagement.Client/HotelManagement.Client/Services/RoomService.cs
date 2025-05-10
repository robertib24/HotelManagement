using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
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