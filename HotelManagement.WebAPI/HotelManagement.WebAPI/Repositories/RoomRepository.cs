using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using HotelManagement.WebAPI.Data;
using HotelManagement.WebAPI.DTOs;
using HotelManagement.WebAPI.Models;
using HotelManagement.WebAPI.Repositories;

namespace HotelManagement.WebAPI.Repositories
{
    public class RoomRepository : Repository<Room>, IRoomRepository
    {
        public RoomRepository(HotelDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<RoomDto>> GetRoomsWithDetailsAsync()
        {
            var rooms = await _context.Rooms
                .Include(r => r.Hotel)
                .Include(r => r.RoomType)
                .ToListAsync();

            return rooms.Select(r => new RoomDto
            {
                Id = r.Id,
                RoomNumber = r.RoomNumber,
                HotelId = r.HotelId,
                HotelName = r.Hotel.Name,
                RoomTypeId = r.RoomTypeId,
                RoomTypeName = r.RoomType.Name,
                Floor = r.Floor,
                IsClean = r.IsClean,
                IsOccupied = r.IsOccupied,
                NeedsRepair = r.NeedsRepair,
                Notes = r.Notes,
                Price = r.RoomType.BasePrice
            }).ToList();
        }

        public async Task<RoomDto> GetRoomDetailsAsync(int id)
        {
            var room = await _context.Rooms
                .Include(r => r.Hotel)
                .Include(r => r.RoomType)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (room == null)
                return null;

            return new RoomDto
            {
                Id = room.Id,
                RoomNumber = room.RoomNumber,
                HotelId = room.HotelId,
                HotelName = room.Hotel.Name,
                RoomTypeId = room.RoomTypeId,
                RoomTypeName = room.RoomType.Name,
                Floor = room.Floor,
                IsClean = room.IsClean,
                IsOccupied = room.IsOccupied,
                NeedsRepair = room.NeedsRepair,
                Notes = room.Notes,
                Price = room.RoomType.BasePrice
            };
        }

        public async Task<IEnumerable<RoomDto>> GetRoomsByHotelAsync(int hotelId)
        {
            var rooms = await _context.Rooms
                .Include(r => r.Hotel)
                .Include(r => r.RoomType)
                .Where(r => r.HotelId == hotelId)
                .ToListAsync();

            return rooms.Select(r => new RoomDto
            {
                Id = r.Id,
                RoomNumber = r.RoomNumber,
                HotelId = r.HotelId,
                HotelName = r.Hotel.Name,
                RoomTypeId = r.RoomTypeId,
                RoomTypeName = r.RoomType.Name,
                Floor = r.Floor,
                IsClean = r.IsClean,
                IsOccupied = r.IsOccupied,
                NeedsRepair = r.NeedsRepair,
                Notes = r.Notes,
                Price = r.RoomType.BasePrice
            }).ToList();
        }

        public async Task<bool> IsRoomAvailableAsync(int roomId, DateTime checkIn, DateTime checkOut)
        {
            var room = await _context.Rooms.FindAsync(roomId);
            if (room == null || room.IsOccupied || room.NeedsRepair)
                return false;

            var overlappingReservations = await _context.Reservations
                .AnyAsync(r => r.RoomId == roomId &&
                        r.CheckOutDate > checkIn &&
                        r.CheckInDate < checkOut &&
                        (r.ReservationStatus == "Confirmed" || r.ReservationStatus == "CheckedIn"));

            return !overlappingReservations;
        }

        public async Task<IEnumerable<RoomDto>> GetAvailableRoomsAsync(int hotelId, int? roomTypeId, DateTime checkIn, DateTime checkOut)
        {
            _context.Configuration.ProxyCreationEnabled = false;
            _context.Configuration.LazyLoadingEnabled = false;

            var query = _context.Rooms
                .AsNoTracking()
                .Include(r => r.Hotel)
                .Include(r => r.RoomType)
                .Where(r => r.HotelId == hotelId &&
                           !r.IsOccupied &&
                           r.IsClean &&
                           !r.NeedsRepair);

            if (roomTypeId.HasValue)
            {
                query = query.Where(r => r.RoomTypeId == roomTypeId.Value);
            }

            var reservedRoomIds = await _context.Reservations
                .Where(r => r.CheckInDate < checkOut &&
                           r.CheckOutDate > checkIn &&
                           r.ReservationStatus != "Cancelled")
                .Select(r => r.RoomId)
                .Distinct()
                .ToListAsync();

            var availableRooms = await query
                .Where(r => !reservedRoomIds.Contains(r.Id))
                .ToListAsync();

            return availableRooms.Select(r => new RoomDto
            {
                Id = r.Id,
                RoomNumber = r.RoomNumber,
                HotelId = r.HotelId,
                HotelName = r.Hotel.Name,
                RoomTypeId = r.RoomTypeId,
                RoomTypeName = r.RoomType.Name,
                Floor = r.Floor,
                Price = r.RoomType.BasePrice,
                IsClean = r.IsClean,
                IsOccupied = r.IsOccupied,
                NeedsRepair = r.NeedsRepair,
                Notes = r.Notes
            });
        }

        public async Task SetRoomStatusAsync(int roomId, bool isOccupied, bool isClean, bool needsRepair)
        {
            var room = await _context.Rooms.FindAsync(roomId);
            if (room != null)
            {
                room.IsOccupied = isOccupied;
                room.IsClean = isClean;
                room.NeedsRepair = needsRepair;
                await _context.SaveChangesAsync();
            }
        }
    }
}