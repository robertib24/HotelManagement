using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using HotelManagement.WebAPI.DTOs;
using HotelManagement.WebAPI.Models;

namespace HotelManagement.WebAPI.Repositories
{
    public interface IHotelRepository : IRepository<Hotel>
    {
        Task<IEnumerable<HotelDto>> GetHotelsWithDetailsAsync();
        Task<HotelDto> GetHotelDetailsAsync(int id);
        Task<int> GetAvailableRoomsCountAsync(int hotelId);
    }
}