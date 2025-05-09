using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using HotelManagement.WebAPI.DTOs;
using HotelManagement.WebAPI.Models;

namespace HotelManagement.WebAPI.Repositories
{
    public interface IRoomTypeRepository : IRepository<RoomType>
    {
        Task<IEnumerable<RoomTypeDto>> GetRoomTypesWithDetailsAsync();
        Task<RoomTypeDto> GetRoomTypeDetailsAsync(int id);
        Task<IEnumerable<RoomTypeDto>> GetRoomTypesByHotelAsync(int hotelId);
    }
}