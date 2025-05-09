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
    public class InvoiceRepository : Repository<Invoice>, IInvoiceRepository
    {
        public InvoiceRepository(HotelDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<InvoiceDto>> GetInvoicesWithDetailsAsync()
        {
            var invoices = await _context.Invoices
                .Include(i => i.Reservation)
                .Include(i => i.Reservation.Customer)
                .Include(i => i.Reservation.Room)
                .Include(i => i.Reservation.Room.Hotel)
                .OrderByDescending(i => i.IssueDate)
                .ToListAsync();

            return invoices.Select(i => new InvoiceDto
            {
                Id = i.Id,
                ReservationId = i.ReservationId,
                InvoiceNumber = i.InvoiceNumber,
                CustomerName = $"{i.Reservation.Customer.FirstName} {i.Reservation.Customer.LastName}",
                RoomInfo = $"{i.Reservation.Room.RoomNumber}",
                HotelName = i.Reservation.Room.Hotel.Name,
                CheckInDate = i.Reservation.CheckInDate,
                CheckOutDate = i.Reservation.CheckOutDate,
                IssueDate = i.IssueDate,
                DueDate = i.DueDate,
                SubTotal = i.SubTotal,
                Tax = i.Tax,
                Total = i.Total,
                IsPaid = i.IsPaid,
                PaymentMethod = i.PaymentMethod,
                PaymentDate = i.PaymentDate
            }).ToList();
        }

        public async Task<InvoiceDto> GetInvoiceDetailsAsync(int id)
        {
            var invoice = await _context.Invoices
                .Include(i => i.Reservation)
                .Include(i => i.Reservation.Customer)
                .Include(i => i.Reservation.Room)
                .Include(i => i.Reservation.Room.Hotel)
                .Include(i => i.Reservation.Room.RoomType)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (invoice == null)
                return null;

            return new InvoiceDto
            {
                Id = invoice.Id,
                ReservationId = invoice.ReservationId,
                InvoiceNumber = invoice.InvoiceNumber,
                CustomerName = $"{invoice.Reservation.Customer.FirstName} {invoice.Reservation.Customer.LastName}",
                RoomInfo = $"{invoice.Reservation.Room.RoomNumber} ({invoice.Reservation.Room.RoomType.Name})",
                HotelName = invoice.Reservation.Room.Hotel.Name,
                CheckInDate = invoice.Reservation.CheckInDate,
                CheckOutDate = invoice.Reservation.CheckOutDate,
                IssueDate = invoice.IssueDate,
                DueDate = invoice.DueDate,
                SubTotal = invoice.SubTotal,
                Tax = invoice.Tax,
                Total = invoice.Total,
                IsPaid = invoice.IsPaid,
                PaymentMethod = invoice.PaymentMethod,
                PaymentDate = invoice.PaymentDate
            };
        }

        public async Task<InvoiceDto> GetInvoiceByReservationAsync(int reservationId)
        {
            var invoice = await _context.Invoices
                .Include(i => i.Reservation)
                .Include(i => i.Reservation.Customer)
                .Include(i => i.Reservation.Room)
                .Include(i => i.Reservation.Room.Hotel)
                .Include(i => i.Reservation.Room.RoomType)
                .FirstOrDefaultAsync(i => i.ReservationId == reservationId);

            if (invoice == null)
                return null;

            return new InvoiceDto
            {
                Id = invoice.Id,
                ReservationId = invoice.ReservationId,
                InvoiceNumber = invoice.InvoiceNumber,
                CustomerName = $"{invoice.Reservation.Customer.FirstName} {invoice.Reservation.Customer.LastName}",
                RoomInfo = $"{invoice.Reservation.Room.RoomNumber} ({invoice.Reservation.Room.RoomType.Name})",
                HotelName = invoice.Reservation.Room.Hotel.Name,
                CheckInDate = invoice.Reservation.CheckInDate,
                CheckOutDate = invoice.Reservation.CheckOutDate,
                IssueDate = invoice.IssueDate,
                DueDate = invoice.DueDate,
                SubTotal = invoice.SubTotal,
                Tax = invoice.Tax,
                Total = invoice.Total,
                IsPaid = invoice.IsPaid,
                PaymentMethod = invoice.PaymentMethod,
                PaymentDate = invoice.PaymentDate
            };
        }

        public async Task<InvoiceDto> GenerateInvoiceAsync(int reservationId)
        {
            var reservation = await _context.Reservations
                .Include(r => r.Customer)
                .Include(r => r.Room)
                .Include(r => r.Room.Hotel)
                .Include(r => r.Room.RoomType)
                .FirstOrDefaultAsync(r => r.Id == reservationId);

            if (reservation == null)
                return null;

            // Check if invoice already exists
            var existingInvoice = await _context.Invoices
                .FirstOrDefaultAsync(i => i.ReservationId == reservationId);

            if (existingInvoice != null)
            {
                return await GetInvoiceByReservationAsync(reservationId);
            }

            // Calculate subtotal, tax, and total
            var subtotal = reservation.TotalPrice;
            var taxRate = 0.19m; // 19% tax rate
            var tax = subtotal * taxRate;
            var total = subtotal + tax;

            // Generate invoice number
            var invoiceNumber = await GenerateInvoiceNumberAsync();

            // Create new invoice
            var invoice = new Invoice
            {
                ReservationId = reservationId,
                InvoiceNumber = invoiceNumber,
                IssueDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(15),
                SubTotal = subtotal,
                Tax = tax,
                Total = total,
                IsPaid = false,
                PaymentMethod = null,
                PaymentDate = null
            };

            _context.Invoices.Add(invoice);
            await _context.SaveChangesAsync();

            return await GetInvoiceByReservationAsync(reservationId);
        }

        public async Task<string> GenerateInvoiceNumberAsync()
        {
            var year = DateTime.Now.Year;
            var month = DateTime.Now.Month.ToString().PadLeft(2, '0');

            var lastInvoice = await _context.Invoices
                .Where(i => i.InvoiceNumber.StartsWith($"INV-{year}-{month}-"))
                .OrderByDescending(i => i.InvoiceNumber)
                .FirstOrDefaultAsync();

            int nextNumber = 1;

            if (lastInvoice != null)
            {
                var lastNumberStr = lastInvoice.InvoiceNumber.Split('-').Last();
                if (int.TryParse(lastNumberStr, out int lastNumber))
                {
                    nextNumber = lastNumber + 1;
                }
            }

            return $"INV-{year}-{month}-{nextNumber.ToString().PadLeft(4, '0')}";
        }

        public async Task<IEnumerable<InvoiceDto>> GetUnpaidInvoicesAsync()
        {
            var invoices = await _context.Invoices
                .Include(i => i.Reservation)
                .Include(i => i.Reservation.Customer)
                .Include(i => i.Reservation.Room)
                .Include(i => i.Reservation.Room.Hotel)
                .Where(i => !i.IsPaid)
                .OrderBy(i => i.DueDate)
                .ToListAsync();

            return invoices.Select(i => new InvoiceDto
            {
                Id = i.Id,
                ReservationId = i.ReservationId,
                InvoiceNumber = i.InvoiceNumber,
                CustomerName = $"{i.Reservation.Customer.FirstName} {i.Reservation.Customer.LastName}",
                RoomInfo = $"{i.Reservation.Room.RoomNumber}",
                HotelName = i.Reservation.Room.Hotel.Name,
                CheckInDate = i.Reservation.CheckInDate,
                CheckOutDate = i.Reservation.CheckOutDate,
                IssueDate = i.IssueDate,
                DueDate = i.DueDate,
                SubTotal = i.SubTotal,
                Tax = i.Tax,
                Total = i.Total,
                IsPaid = i.IsPaid,
                PaymentMethod = i.PaymentMethod,
                PaymentDate = i.PaymentDate
            }).ToList();
        }

        public async Task<IEnumerable<InvoiceDto>> GetPaidInvoicesAsync()
        {
            var invoices = await _context.Invoices
                .Include(i => i.Reservation)
                .Include(i => i.Reservation.Customer)
                .Include(i => i.Reservation.Room)
                .Include(i => i.Reservation.Room.Hotel)
                .Where(i => i.IsPaid)
                .OrderByDescending(i => i.PaymentDate)
                .ToListAsync();

            return invoices.Select(i => new InvoiceDto
            {
                Id = i.Id,
                ReservationId = i.ReservationId,
                InvoiceNumber = i.InvoiceNumber,
                CustomerName = $"{i.Reservation.Customer.FirstName} {i.Reservation.Customer.LastName}",
                RoomInfo = $"{i.Reservation.Room.RoomNumber}",
                HotelName = i.Reservation.Room.Hotel.Name,
                CheckInDate = i.Reservation.CheckInDate,
                CheckOutDate = i.Reservation.CheckOutDate,
                IssueDate = i.IssueDate,
                DueDate = i.DueDate,
                SubTotal = i.SubTotal,
                Tax = i.Tax,
                Total = i.Total,
                IsPaid = i.IsPaid,
                PaymentMethod = i.PaymentMethod,
                PaymentDate = i.PaymentDate
            }).ToList();
        }

        public async Task<bool> MarkAsPaidAsync(int invoiceId, string paymentMethod)
        {
            var invoice = await _context.Invoices.FindAsync(invoiceId);
            if (invoice == null)
                return false;

            invoice.IsPaid = true;
            invoice.PaymentMethod = paymentMethod;
            invoice.PaymentDate = DateTime.Now;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}