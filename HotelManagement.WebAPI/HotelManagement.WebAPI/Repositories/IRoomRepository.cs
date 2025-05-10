using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using HotelManagement.WebAPI.DTOs;
using HotelManagement.WebAPI.Models;

namespace HotelManagement.WebAPI.Repositories
{
    public interface IRoomRepository : IRepository<Room>
    {
        Task<IEnumerable<RoomDto>> GetRoomsWithDetailsAsync();
        Task<RoomDto> GetRoomDetailsAsync(int id);

        Task<IEnumerable<RoomDto>> GetAvailableRoomsAsync(int hotelId, int? roomTypeId, DateTime checkIn, DateTime checkOut);
        Task<IEnumerable<RoomDto>> GetRoomsByHotelAsync(int hotelId);
        Task<bool> IsRoomAvailableAsync(int roomId, DateTime checkIn, DateTime checkOut);
        Task SetRoomStatusAsync(int roomId, bool isOccupied, bool isClean, bool needsRepair);
    }
}