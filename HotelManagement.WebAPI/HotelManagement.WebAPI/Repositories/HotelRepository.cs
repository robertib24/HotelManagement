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
    public class HotelRepository : Repository<Hotel>, IHotelRepository
    {
        public HotelRepository(HotelDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<HotelDto>> GetHotelsWithDetailsAsync()
        {
            var hotels = await _context.Hotels
                .Include(h => h.Rooms)
                .ToListAsync();

            return hotels.Select(h => new HotelDto
            {
                Id = h.Id,
                Name = h.Name,
                Address = h.Address,
                City = h.City,
                Country = h.Country,
                Stars = h.Stars,
                ContactPhone = h.ContactPhone,
                Email = h.Email,
                IsActive = h.IsActive,
                TotalRooms = h.Rooms.Count,
                AvailableRooms = h.Rooms.Count(r => !r.IsOccupied && !r.NeedsRepair)
            }).ToList();
        }

        public async Task<HotelDto> GetHotelDetailsAsync(int id)
        {
            var hotel = await _context.Hotels
                .Include(h => h.Rooms)
                .FirstOrDefaultAsync(h => h.Id == id);

            if (hotel == null)
                return null;

            return new HotelDto
            {
                Id = hotel.Id,
                Name = hotel.Name,
                Address = hotel.Address,
                City = hotel.City,
                Country = hotel.Country,
                Stars = hotel.Stars,
                ContactPhone = hotel.ContactPhone,
                Email = hotel.Email,
                IsActive = hotel.IsActive,
                TotalRooms = hotel.Rooms.Count,
                AvailableRooms = hotel.Rooms.Count(r => !r.IsOccupied && !r.NeedsRepair)
            };
        }

        public async Task<int> GetAvailableRoomsCountAsync(int hotelId)
        {
            return await _context.Rooms
                .CountAsync(r => r.HotelId == hotelId && !r.IsOccupied && !r.NeedsRepair);
        }
    }
}