using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using HotelManagement.WebAPI.Data;
using HotelManagement.WebAPI.DTOs;
using HotelManagement.WebAPI.Models;

namespace HotelManagement.WebAPI.Repositories
{
    public class RoomTypeRepository : Repository<RoomType>, IRoomTypeRepository
    {
        public RoomTypeRepository(HotelDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<RoomTypeDto>> GetRoomTypesWithDetailsAsync()
        {
            var roomTypes = await _context.RoomTypes
                .Include(rt => rt.Rooms)
                .ToListAsync();

            return roomTypes.Select(rt => new RoomTypeDto
            {
                Id = rt.Id,
                Name = rt.Name,
                Description = rt.Description,
                MaxOccupancy = rt.MaxOccupancy,
                BasePrice = rt.BasePrice,
                AvailableRooms = rt.Rooms.Count(r => !r.IsOccupied && !r.NeedsRepair)
            }).ToList();
        }

        public async Task<RoomTypeDto> GetRoomTypeDetailsAsync(int id)
        {
            var roomType = await _context.RoomTypes
                .Include(rt => rt.Rooms)
                .FirstOrDefaultAsync(rt => rt.Id == id);

            if (roomType == null)
                return null;

            return new RoomTypeDto
            {
                Id = roomType.Id,
                Name = roomType.Name,
                Description = roomType.Description,
                MaxOccupancy = roomType.MaxOccupancy,
                BasePrice = roomType.BasePrice,
                AvailableRooms = roomType.Rooms.Count(r => !r.IsOccupied && !r.NeedsRepair)
            };
        }

        public async Task<IEnumerable<RoomTypeDto>> GetRoomTypesByHotelAsync(int hotelId)
        {
            var roomTypeIds = await _context.Rooms
                .Where(r => r.HotelId == hotelId)
                .Select(r => r.RoomTypeId)
                .Distinct()
                .ToListAsync();

            var roomTypes = await _context.RoomTypes
                .Include(rt => rt.Rooms)
                .Where(rt => roomTypeIds.Contains(rt.Id))
                .ToListAsync();

            return roomTypes.Select(rt => new RoomTypeDto
            {
                Id = rt.Id,
                Name = rt.Name,
                Description = rt.Description,
                MaxOccupancy = rt.MaxOccupancy,
                BasePrice = rt.BasePrice,
                AvailableRooms = rt.Rooms.Count(r => r.HotelId == hotelId && !r.IsOccupied && !r.NeedsRepair)
            }).ToList();
        }
    }
}