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
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(HotelDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<CustomerDto>> GetCustomersWithDetailsAsync()
        {
            var customers = await _context.Customers
                .Include(c => c.Reservations)
                .ToListAsync();

            return customers.Select(c => new CustomerDto
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Email = c.Email,
                Phone = c.Phone,
                Address = c.Address,
                City = c.City,
                Country = c.Country,
                IdNumber = c.IdNumber,
                DateOfBirth = c.DateOfBirth,
                RegistrationDate = c.RegistrationDate,
                IsVIP = c.IsVIP,
                TotalStays = c.Reservations.Count(r => r.ReservationStatus == "Completed"),
                TotalSpent = c.Reservations.Sum(r => r.TotalPrice)
            }).ToList();
        }

        public async Task<CustomerDto> GetCustomerDetailsAsync(int id)
        {
            var customer = await _context.Customers
                .Include(c => c.Reservations)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (customer == null)
                return null;

            return new CustomerDto
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                Phone = customer.Phone,
                Address = customer.Address,
                City = customer.City,
                Country = customer.Country,
                IdNumber = customer.IdNumber,
                DateOfBirth = customer.DateOfBirth,
                RegistrationDate = customer.RegistrationDate,
                IsVIP = customer.IsVIP,
                TotalStays = customer.Reservations.Count(r => r.ReservationStatus == "Completed"),
                TotalSpent = customer.Reservations.Sum(r => r.TotalPrice)
            };
        }

        public async Task<IEnumerable<CustomerDto>> GetVIPCustomersAsync()
        {
            var customers = await _context.Customers
                .Include(c => c.Reservations)
                .Where(c => c.IsVIP)
                .ToListAsync();

            return customers.Select(c => new CustomerDto
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Email = c.Email,
                Phone = c.Phone,
                Address = c.Address,
                City = c.City,
                Country = c.Country,
                IdNumber = c.IdNumber,
                DateOfBirth = c.DateOfBirth,
                RegistrationDate = c.RegistrationDate,
                IsVIP = c.IsVIP,
                TotalStays = c.Reservations.Count(r => r.ReservationStatus == "Completed"),
                TotalSpent = c.Reservations.Sum(r => r.TotalPrice)
            }).ToList();
        }

        public async Task<CustomerDto> GetCustomerByEmailAsync(string email)
        {
            var customer = await _context.Customers
                .Include(c => c.Reservations)
                .FirstOrDefaultAsync(c => c.Email == email);

            if (customer == null)
                return null;

            return new CustomerDto
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                Phone = customer.Phone,
                Address = customer.Address,
                City = customer.City,
                Country = customer.Country,
                IdNumber = customer.IdNumber,
                DateOfBirth = customer.DateOfBirth,
                RegistrationDate = customer.RegistrationDate,
                IsVIP = customer.IsVIP,
                TotalStays = customer.Reservations.Count(r => r.ReservationStatus == "Completed"),
                TotalSpent = customer.Reservations.Sum(r => r.TotalPrice)
            };
        }

        public async Task<CustomerDto> GetCustomerByIdNumberAsync(string idNumber)
        {
            var customer = await _context.Customers
                .Include(c => c.Reservations)
                .FirstOrDefaultAsync(c => c.IdNumber == idNumber);

            if (customer == null)
                return null;

            return new CustomerDto
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                Phone = customer.Phone,
                Address = customer.Address,
                City = customer.City,
                Country = customer.Country,
                IdNumber = customer.IdNumber,
                DateOfBirth = customer.DateOfBirth,
                RegistrationDate = customer.RegistrationDate,
                IsVIP = customer.IsVIP,
                TotalStays = customer.Reservations.Count(r => r.ReservationStatus == "Completed"),
                TotalSpent = customer.Reservations.Sum(r => r.TotalPrice)
            };
        }

        public async Task<IEnumerable<ReservationDto>> GetCustomerReservationsAsync(int customerId)
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
    }
}