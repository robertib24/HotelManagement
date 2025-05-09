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
    public class ReservationServiceRepository : Repository<ReservationService>, IReservationServiceRepository
    {
        public ReservationServiceRepository(HotelDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<ReservationServiceDto>> GetServicesByReservationAsync(int reservationId)
        {
            var reservationServices = await _context.ReservationServices
                .Include(rs => rs.Service)
                .Where(rs => rs.ReservationId == reservationId)
                .ToListAsync();

            return reservationServices.Select(rs => new ReservationServiceDto
            {
                Id = rs.Id,
                ReservationId = rs.ReservationId,
                ServiceId = rs.ServiceId,
                ServiceName = rs.Service.Name,
                ServiceCategory = rs.Service.Category,
                Date = rs.Date,
                Quantity = rs.Quantity,
                UnitPrice = rs.Service.Price,
                TotalPrice = rs.TotalPrice,
                Notes = rs.Notes
            }).ToList();
        }

        public async Task<decimal> GetTotalServicesCostAsync(int reservationId)
        {
            return await _context.ReservationServices
                .Where(rs => rs.ReservationId == reservationId)
                .SumAsync(rs => rs.TotalPrice);
        }

        public async Task AddServiceToReservationAsync(int reservationId, int serviceId, int quantity, DateTime date, string notes)
        {
            var service = await _context.Services.FindAsync(serviceId);
            if (service == null)
                return;

            var reservationService = new ReservationService
            {
                ReservationId = reservationId,
                ServiceId = serviceId,
                Date = date,
                Quantity = quantity,
                TotalPrice = service.Price * quantity,
                Notes = notes
            };

            _context.ReservationServices.Add(reservationService);
            await _context.SaveChangesAsync();

            var reservation = await _context.Reservations.FindAsync(reservationId);
            if (reservation != null)
            {
                var totalServicesCost = await GetTotalServicesCostAsync(reservationId);
                var roomCost = await _context.Rooms
                    .Include(r => r.RoomType)
                    .Where(r => r.Id == reservation.RoomId)
                    .Select(r => r.RoomType.BasePrice * ((reservation.CheckOutDate - reservation.CheckInDate).Days))
                    .FirstOrDefaultAsync();

                reservation.TotalPrice = roomCost + totalServicesCost;
                await _context.SaveChangesAsync();
            }
        }

        public async Task RemoveServiceFromReservationAsync(int reservationServiceId)
        {
            var reservationService = await _context.ReservationServices.FindAsync(reservationServiceId);
            if (reservationService == null)
                return;

            int reservationId = reservationService.ReservationId;
            decimal serviceCost = reservationService.TotalPrice;

            _context.ReservationServices.Remove(reservationService);
            await _context.SaveChangesAsync();

            var reservation = await _context.Reservations.FindAsync(reservationId);
            if (reservation != null)
            {
                reservation.TotalPrice -= serviceCost;
                await _context.SaveChangesAsync();
            }
        }
    }
}