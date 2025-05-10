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
            // Verificăm dacă camera există și nu necesită reparații
            var room = await _context.Rooms.FindAsync(roomId);
            if (room == null || room.NeedsRepair)
                return false;

            // Verificăm dacă există rezervări care se suprapun
            var overlappingReservations = await _context.Reservations
                .AnyAsync(r => r.RoomId == roomId &&
                        r.CheckOutDate > checkIn &&
                        r.CheckInDate < checkOut &&
                        (r.ReservationStatus == "Confirmed" || r.ReservationStatus == "CheckedIn"));

            return !overlappingReservations;
        }

        public async Task<IEnumerable<RoomDto>> GetAvailableRoomsAsync(int hotelId, int? roomTypeId, DateTime checkIn, DateTime checkOut)
        {
            try
            {
                _context.Configuration.ProxyCreationEnabled = false;
                _context.Configuration.LazyLoadingEnabled = false;

                // Selectăm camerele din hotelul specificat care nu necesită reparații
                var query = _context.Rooms
                    .AsNoTracking()
                    .Include(r => r.Hotel)
                    .Include(r => r.RoomType)
                    .Where(r => r.HotelId == hotelId && !r.NeedsRepair);

                // Filtrăm după tipul de cameră dacă este specificat
                if (roomTypeId.HasValue)
                {
                    query = query.Where(r => r.RoomTypeId == roomTypeId.Value);
                }

                // Obținem toate camerele care îndeplinesc condițiile inițiale
                var potentialRooms = await query.ToListAsync();

                // Obținem toate rezervările active care se suprapun cu perioada specificată
                var overlappingReservations = await _context.Reservations
                    .Where(r => r.CheckOutDate > checkIn &&
                            r.CheckInDate < checkOut &&
                            r.ReservationStatus != "Cancelled" &&
                            r.ReservationStatus != "Completed")
                    .ToListAsync();

                // Extragem ID-urile camerelor rezervate
                var reservedRoomIds = overlappingReservations.Select(r => r.RoomId).Distinct().ToList();

                // Filtrăm camerele disponibile
                var availableRooms = potentialRooms.Where(r => !reservedRoomIds.Contains(r.Id)).ToList();

                // Convertim camerele disponibile în DTO-uri
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
            catch (Exception ex)
            {
                // Adăugăm logging pentru a detecta erorile
                System.Diagnostics.Debug.WriteLine($"Error in GetAvailableRoomsAsync: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");
                throw; // Aruncăm excepția mai departe pentru a fi tratată de apelant
            }
        }

        public async Task SetRoomStatusAsync(int roomId, bool isOccupied, bool isClean, bool needsRepair)
        {
            try
            {
                var room = await _context.Rooms.FindAsync(roomId);
                if (room != null)
                {
                    room.IsOccupied = isOccupied;
                    room.IsClean = isClean;
                    room.NeedsRepair = needsRepair;
                    await _context.SaveChangesAsync();
                }
                else
                {
                    // Logging pentru cazul în care camera nu este găsită
                    System.Diagnostics.Debug.WriteLine($"Warning: Attempted to set status for non-existent room (ID: {roomId})");
                }
            }
            catch (Exception ex)
            {
                // Logging pentru excepții
                System.Diagnostics.Debug.WriteLine($"Error in SetRoomStatusAsync: {ex.Message}");
                throw; // Aruncăm excepția mai departe
            }
        }
    }
}