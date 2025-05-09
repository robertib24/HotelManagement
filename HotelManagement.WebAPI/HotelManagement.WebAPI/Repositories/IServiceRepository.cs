using System.Collections.Generic;
using System.Threading.Tasks;
using HotelManagement.WebAPI.DTOs;
using HotelManagement.WebAPI.Models;
using HotelManagement.WebAPI.Repositories;

namespace HotelManagement.WebAPI.Repositories
{
    public interface IServiceRepository : IRepository<Service>
    {
        Task<IEnumerable<ServiceDto>> GetServicesWithDetailsAsync();
        Task<ServiceDto> GetServiceDetailsAsync(int id);
        Task<IEnumerable<ServiceDto>> GetServicesByCategoryAsync(string category);
        Task<IEnumerable<ServiceDto>> GetAvailableServicesAsync();
    }
}