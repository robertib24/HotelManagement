using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using HotelManagement.Client.Models;
using Newtonsoft.Json;

namespace HotelManagement.Client.Services
{
    public class RoomTypeService
    {
        public async Task<List<RoomTypeModel>> GetAllRoomTypesAsync()
        {
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync("roomtypes"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<RoomTypeModel>>(result);
                }
                else
                {
                    throw new Exception($"Error: {response.ReasonPhrase}");
                }
            }
        }

        public async Task<RoomTypeModel> GetRoomTypeByIdAsync(int id)
        {
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync($"roomtypes/{id}"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<RoomTypeModel>(result);
                }
                else
                {
                    throw new Exception($"Error: {response.ReasonPhrase}");
                }
            }
        }

        public async Task<List<RoomTypeModel>> GetRoomTypesByHotelAsync(int hotelId)
        {
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync($"roomtypes/hotel/{hotelId}"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<RoomTypeModel>>(result);
                }
                else
                {
                    throw new Exception($"Error: {response.ReasonPhrase}");
                }
            }
        }

        public async Task<RoomTypeModel> CreateRoomTypeAsync(RoomTypeModel roomType)
        {
            var content = new StringContent(JsonConvert.SerializeObject(roomType), System.Text.Encoding.UTF8, "application/json");

            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync("roomtypes", content))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<RoomTypeModel>(result);
                }
                else
                {
                    throw new Exception($"Error: {response.ReasonPhrase}");
                }
            }
        }

        public async Task UpdateRoomTypeAsync(RoomTypeModel roomType)
        {
            var content = new StringContent(JsonConvert.SerializeObject(roomType), System.Text.Encoding.UTF8, "application/json");

            using (HttpResponseMessage response = await ApiHelper.ApiClient.PutAsync($"roomtypes/{roomType.Id}", content))
            {
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Error: {response.ReasonPhrase}");
                }
            }
        }

        public async Task DeleteRoomTypeAsync(int id)
        {
            using (HttpResponseMessage response = await ApiHelper.ApiClient.DeleteAsync($"roomtypes/{id}"))
            {
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Error: {response.ReasonPhrase}");
                }
            }
        }
    }
}