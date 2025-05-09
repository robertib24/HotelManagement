using System.Collections.Generic;
using System.Threading.Tasks;
using HotelManagement.WebAPI.DTOs;
using HotelManagement.WebAPI.Models;
using HotelManagement.WebAPI.Repositories;

namespace HotelManagement.WebAPI.Repositories
{
    public interface IInvoiceRepository : IRepository<Invoice>
    {
        Task<IEnumerable<InvoiceDto>> GetInvoicesWithDetailsAsync();
        Task<InvoiceDto> GetInvoiceDetailsAsync(int id);
        Task<InvoiceDto> GetInvoiceByReservationAsync(int reservationId);
        Task<InvoiceDto> GenerateInvoiceAsync(int reservationId);
        Task<string> GenerateInvoiceNumberAsync();
        Task<IEnumerable<InvoiceDto>> GetUnpaidInvoicesAsync();
        Task<IEnumerable<InvoiceDto>> GetPaidInvoicesAsync();
        Task<bool> MarkAsPaidAsync(int invoiceId, string paymentMethod);
    }
}