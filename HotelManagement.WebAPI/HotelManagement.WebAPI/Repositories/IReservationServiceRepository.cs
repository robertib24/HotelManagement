using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HotelManagement.WebAPI.DTOs;
using HotelManagement.WebAPI.Models;
using HotelManagement.WebAPI.Repositories;

namespace HotelManagement.WebAPI.Repositories
{
    public interface IReservationServiceRepository : IRepository<ReservationService>
    {
        Task<IEnumerable<ReservationServiceDto>> GetServicesByReservationAsync(int reservationId);
        Task<decimal> GetTotalServicesCostAsync(int reservationId);
        Task AddServiceToReservationAsync(int reservationId, int serviceId, int quantity, DateTime date, string notes);
        Task RemoveServiceFromReservationAsync(int reservationServiceId);
    }
}