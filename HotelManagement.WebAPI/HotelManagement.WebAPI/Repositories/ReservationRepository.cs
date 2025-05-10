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
    public class ReservationRepository : Repository<Reservation>, IReservationRepository
    {
        public ReservationRepository(HotelDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<ReservationDto>> GetReservationsWithDetailsAsync()
        {
            var reservations = await _context.Reservations
                .Include(r => r.Room)
                .Include(r => r.Room.Hotel)
                .Include(r => r.Room.RoomType)
                .Include(r => r.Customer)
                .Include(r => r.Invoice)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();

            return reservations.Select(r => new ReservationDto
            {
                Id = r.Id,
                CustomerId = r.CustomerId,
                CustomerName = $"{r.Customer.FirstName} {r.Customer.LastName}",
                RoomId = r.RoomId,
                RoomNumber = r.Room.RoomNumber,
                HotelId = r.Room.HotelId,
                HotelName = r.Room.Hotel.Name,
                CheckInDate = r.CheckInDate,
                CheckOutDate = r.CheckOutDate,
                NumberOfGuests = r.NumberOfGuests,
                TotalPrice = r.TotalPrice,
                ReservationStatus = r.ReservationStatus,
                CreatedAt = r.CreatedAt,
                Notes = r.Notes,
                IsPaid = r.Invoice != null && r.Invoice.IsPaid
            }).ToList();
        }

        public async Task<ReservationDto> GetReservationDetailsAsync(int id)
        {
            var reservation = await _context.Reservations
                .Include(r => r.Room)
                .Include(r => r.Room.Hotel)
                .Include(r => r.Room.RoomType)
                .Include(r => r.Customer)
                .Include(r => r.Invoice)
                .Include(r => r.ReservationServices)
                .Include(r => r.ReservationServices.Select(rs => rs.Service))
                .FirstOrDefaultAsync(r => r.Id == id);

            if (reservation == null)
                return null;

            var services = reservation.ReservationServices
                .Select(rs => new ServiceDto
                {
                    Id = rs.Service.Id,
                    Name = rs.Service.Name,
                    Description = rs.Service.Description,
                    Price = rs.Service.Price,
                    Category = rs.Service.Category,
                    IsAvailable = rs.Service.IsAvailable
                }).ToList();

            return new ReservationDto
            {
                Id = reservation.Id,
                CustomerId = reservation.CustomerId,
                CustomerName = $"{reservation.Customer.FirstName} {reservation.Customer.LastName}",
                RoomId = reservation.RoomId,
                RoomNumber = reservation.Room.RoomNumber,
                HotelId = reservation.Room.HotelId,
                HotelName = reservation.Room.Hotel.Name,
                CheckInDate = reservation.CheckInDate,
                CheckOutDate = reservation.CheckOutDate,
                NumberOfGuests = reservation.NumberOfGuests,
                TotalPrice = reservation.TotalPrice,
                ReservationStatus = reservation.ReservationStatus,
                CreatedAt = reservation.CreatedAt,
                Notes = reservation.Notes,
                Services = services,
                IsPaid = reservation.Invoice != null && reservation.Invoice.IsPaid
            };
        }

        public async Task<IEnumerable<ReservationDto>> GetReservationsByCustomerAsync(int customerId)
        {
            var reservations = await _context.Reservations
                .Include(r => r.Room)
                .Include(r => r.Room.Hotel)
                .Include(r => r.Room.RoomType)
                .Include(r => r.Customer)
                .Include(r => r.Invoice)
                .Where(r => r.CustomerId == customerId)
                .OrderByDescending(r => r.CheckInDate)
                .ToListAsync();

            return reservations.Select(r => new ReservationDto
            {
                Id = r.Id,
                CustomerId = r.CustomerId,
                CustomerName = $"{r.Customer.FirstName} {r.Customer.LastName}",
                RoomId = r.RoomId,
                RoomNumber = r.Room.RoomNumber,
                HotelId = r.Room.HotelId,
                HotelName = r.Room.Hotel.Name,
                CheckInDate = r.CheckInDate,
                CheckOutDate = r.CheckOutDate,
                NumberOfGuests = r.NumberOfGuests,
                TotalPrice = r.TotalPrice,
                ReservationStatus = r.ReservationStatus,
                CreatedAt = r.CreatedAt,
                Notes = r.Notes,
                IsPaid = r.Invoice != null && r.Invoice.IsPaid
            }).ToList();
        }

        public async Task<IEnumerable<ReservationDto>> GetReservationsByHotelAsync(int hotelId)
        {
            var reservations = await _context.Reservations
                .Include(r => r.Room)
                .Include(r => r.Room.Hotel)
                .Include(r => r.Room.RoomType)
                .Include(r => r.Customer)
                .Include(r => r.Invoice)
                .Where(r => r.Room.HotelId == hotelId)
                .OrderByDescending(r => r.CheckInDate)
                .ToListAsync();

            return reservations.Select(r => new ReservationDto
            {
                Id = r.Id,
                CustomerId = r.CustomerId,
                CustomerName = $"{r.Customer.FirstName} {r.Customer.LastName}",
                RoomId = r.RoomId,
                RoomNumber = r.Room.RoomNumber,
                HotelId = r.Room.HotelId,
                HotelName = r.Room.Hotel.Name,
                CheckInDate = r.CheckInDate,
                CheckOutDate = r.CheckOutDate,
                NumberOfGuests = r.NumberOfGuests,
                TotalPrice = r.TotalPrice,
                ReservationStatus = r.ReservationStatus,
                CreatedAt = r.CreatedAt,
                Notes = r.Notes,
                IsPaid = r.Invoice != null && r.Invoice.IsPaid
            }).ToList();
        }

        public async Task<IEnumerable<ReservationDto>> GetActiveReservationsAsync()
        {
            var reservations = await _context.Reservations
                .Include(r => r.Room)
                .Include(r => r.Room.Hotel)
                .Include(r => r.Room.RoomType)
                .Include(r => r.Customer)
                .Include(r => r.Invoice)
                .Where(r => (r.ReservationStatus == "Confirmed" || r.ReservationStatus == "CheckedIn") &&
                           r.CheckOutDate >= DateTime.Now)
                .OrderBy(r => r.CheckInDate)
                .ToListAsync();

            return reservations.Select(r => new ReservationDto
            {
                Id = r.Id,
                CustomerId = r.CustomerId,
                CustomerName = $"{r.Customer.FirstName} {r.Customer.LastName}",
                RoomId = r.RoomId,
                RoomNumber = r.Room.RoomNumber,
                HotelId = r.Room.HotelId,
                HotelName = r.Room.Hotel.Name,
                CheckInDate = r.CheckInDate,
                CheckOutDate = r.CheckOutDate,
                NumberOfGuests = r.NumberOfGuests,
                TotalPrice = r.TotalPrice,
                ReservationStatus = r.ReservationStatus,
                CreatedAt = r.CreatedAt,
                Notes = r.Notes,
                IsPaid = r.Invoice != null && r.Invoice.IsPaid
            }).ToList();
        }

        public async Task<IEnumerable<ReservationDto>> GetCheckInsForTodayAsync()
        {
            var today = DateTime.Today;
            var tomorrow = today.AddDays(1);

            var reservations = await _context.Reservations
                .Include(r => r.Room)
                .Include(r => r.Room.Hotel)
                .Include(r => r.Room.RoomType)
                .Include(r => r.Customer)
                .Include(r => r.Invoice)
                .Where(r => r.CheckInDate >= today && r.CheckInDate < tomorrow &&
                           r.ReservationStatus == "Confirmed")
                .OrderBy(r => r.CheckInDate)
                .ToListAsync();

            return reservations.Select(r => new ReservationDto
            {
                Id = r.Id,
                CustomerId = r.CustomerId,
                CustomerName = $"{r.Customer.FirstName} {r.Customer.LastName}",
                RoomId = r.RoomId,
                RoomNumber = r.Room.RoomNumber,
                HotelId = r.Room.HotelId,
                HotelName = r.Room.Hotel.Name,
                CheckInDate = r.CheckInDate,
                CheckOutDate = r.CheckOutDate,
                NumberOfGuests = r.NumberOfGuests,
                TotalPrice = r.TotalPrice,
                ReservationStatus = r.ReservationStatus,
                CreatedAt = r.CreatedAt,
                Notes = r.Notes,
                IsPaid = r.Invoice != null && r.Invoice.IsPaid
            }).ToList();
        }

        public async Task<IEnumerable<ReservationDto>> GetCheckOutsForTodayAsync()
        {
            var today = DateTime.Today;
            var tomorrow = today.AddDays(1);

            var reservations = await _context.Reservations
                .Include(r => r.Room)
                .Include(r => r.Room.Hotel)
                .Include(r => r.Room.RoomType)
                .Include(r => r.Customer)
                .Include(r => r.Invoice)
                .Where(r => r.CheckOutDate >= today && r.CheckOutDate < tomorrow &&
                           r.ReservationStatus == "CheckedIn")
                .OrderBy(r => r.CheckOutDate)
                .ToListAsync();

            return reservations.Select(r => new ReservationDto
            {
                Id = r.Id,
                CustomerId = r.CustomerId,
                CustomerName = $"{r.Customer.FirstName} {r.Customer.LastName}",
                RoomId = r.RoomId,
                RoomNumber = r.Room.RoomNumber,
                HotelId = r.Room.HotelId,
                HotelName = r.Room.Hotel.Name,
                CheckInDate = r.CheckInDate,
                CheckOutDate = r.CheckOutDate,
                NumberOfGuests = r.NumberOfGuests,
                TotalPrice = r.TotalPrice,
                ReservationStatus = r.ReservationStatus,
                CreatedAt = r.CreatedAt,
                Notes = r.Notes,
                IsPaid = r.Invoice != null && r.Invoice.IsPaid
            }).ToList();
        }

        public async Task<decimal> CalculateTotalPriceAsync(int roomId, DateTime checkIn, DateTime checkOut)
        {
            var room = await _context.Rooms
                .Include(r => r.RoomType)
                .FirstOrDefaultAsync(r => r.Id == roomId);

            if (room == null)
                return 0;

            int numberOfDays = (int)(checkOut - checkIn).TotalDays;
            return room.RoomType.BasePrice * numberOfDays;
        }

        public async Task<decimal> GetDailyRevenueAsync(DateTime date)
        {
            try
            {
                // Calculează venitul din rezervările active pentru ziua respectivă
                var dailyRevenue = await _context.Reservations
                    .Where(r => r.CheckInDate.Date <= date.Date && r.CheckOutDate.Date >= date.Date &&
                          (r.ReservationStatus == "CheckedIn" || r.ReservationStatus == "Completed"))
                    .SumAsync(r => r.TotalPrice / ((r.CheckOutDate - r.CheckInDate).Days > 0 ?
                                                   (r.CheckOutDate - r.CheckInDate).Days : 1));

                return dailyRevenue;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error calculating daily revenue: {ex.Message}");
                return 0; // Returnează 0 în caz de eroare
            }
        }

        public async Task<bool> IsRoomAvailableForReservationAsync(int roomId, DateTime checkIn, DateTime checkOut, int? excludeReservationId = null)
        {
            var room = await _context.Rooms.FindAsync(roomId);
            if (room == null || room.NeedsRepair)
                return false;

            var query = _context.Reservations
                .Where(r => r.RoomId == roomId &&
                           r.CheckOutDate > checkIn &&
                           r.CheckInDate < checkOut &&
                           (r.ReservationStatus == "Confirmed" || r.ReservationStatus == "CheckedIn"));

            if (excludeReservationId.HasValue)
            {
                query = query.Where(r => r.Id != excludeReservationId.Value);
            }

            var overlappingReservations = await query.AnyAsync();

            return !overlappingReservations;
        }
    }
}