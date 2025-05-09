using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HotelManagement.WebAPI.DTOs;
using HotelManagement.WebAPI.Models;
using HotelManagement.WebAPI.Repositories;

namespace HotelManagement.WebAPI.Repositories
{
    public interface IReservationRepository : IRepository<Reservation>
    {
        Task<IEnumerable<ReservationDto>> GetReservationsWithDetailsAsync();
        Task<ReservationDto> GetReservationDetailsAsync(int id);
        Task<IEnumerable<ReservationDto>> GetReservationsByCustomerAsync(int customerId);
        Task<IEnumerable<ReservationDto>> GetReservationsByHotelAsync(int hotelId);
        Task<IEnumerable<ReservationDto>> GetActiveReservationsAsync();
        Task<IEnumerable<ReservationDto>> GetCheckInsForTodayAsync();
        Task<IEnumerable<ReservationDto>> GetCheckOutsForTodayAsync();
        Task<decimal> CalculateTotalPriceAsync(int roomId, DateTime checkIn, DateTime checkOut);
        Task<bool> IsRoomAvailableForReservationAsync(int roomId, DateTime checkIn, DateTime checkOut, int? excludeReservationId = null);
    }
}