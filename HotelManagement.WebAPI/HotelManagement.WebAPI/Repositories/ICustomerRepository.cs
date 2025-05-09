using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using HotelManagement.WebAPI.DTOs;
using HotelManagement.WebAPI.Models;

namespace HotelManagement.WebAPI.Repositories
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<IEnumerable<CustomerDto>> GetCustomersWithDetailsAsync();
        Task<CustomerDto> GetCustomerDetailsAsync(int id);
        Task<IEnumerable<CustomerDto>> GetVIPCustomersAsync();
        Task<CustomerDto> GetCustomerByEmailAsync(string email);
        Task<CustomerDto> GetCustomerByIdNumberAsync(string idNumber);
        Task<IEnumerable<ReservationDto>> GetCustomerReservationsAsync(int customerId);
    }
}