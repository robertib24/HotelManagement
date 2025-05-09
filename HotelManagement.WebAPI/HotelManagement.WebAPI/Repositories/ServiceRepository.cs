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
    public class ServiceRepository : Repository<Service>, IServiceRepository
    {
        public ServiceRepository(HotelDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<ServiceDto>> GetServicesWithDetailsAsync()
        {
            var services = await _context.Services.ToListAsync();

            return services.Select(s => new ServiceDto
            {
                Id = s.Id,
                Name = s.Name,
                Description = s.Description,
                Price = s.Price,
                Category = s.Category,
                IsAvailable = s.IsAvailable
            }).ToList();
        }

        public async Task<ServiceDto> GetServiceDetailsAsync(int id)
        {
            var service = await _context.Services.FindAsync(id);

            if (service == null)
                return null;

            return new ServiceDto
            {
                Id = service.Id,
                Name = service.Name,
                Description = service.Description,
                Price = service.Price,
                Category = service.Category,
                IsAvailable = service.IsAvailable
            };
        }

        public async Task<IEnumerable<ServiceDto>> GetServicesByCategoryAsync(string category)
        {
            var services = await _context.Services
                .Where(s => s.Category == category)
                .ToListAsync();

            return services.Select(s => new ServiceDto
            {
                Id = s.Id,
                Name = s.Name,
                Description = s.Description,
                Price = s.Price,
                Category = s.Category,
                IsAvailable = s.IsAvailable
            }).ToList();
        }

        public async Task<IEnumerable<ServiceDto>> GetAvailableServicesAsync()
        {
            var services = await _context.Services
                .Where(s => s.IsAvailable)
                .ToListAsync();

            return services.Select(s => new ServiceDto
            {
                Id = s.Id,
                Name = s.Name,
                Description = s.Description,
                Price = s.Price,
                Category = s.Category,
                IsAvailable = s.IsAvailable
            }).ToList();
        }
    }
}