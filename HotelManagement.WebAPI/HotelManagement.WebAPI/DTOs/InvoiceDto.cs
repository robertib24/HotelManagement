using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelManagement.WebAPI.DTOs
{
    public class InvoiceDto
    {
        public int Id { get; set; }
        public int ReservationId { get; set; }
        public string InvoiceNumber { get; set; }
        public string CustomerName { get; set; }
        public string RoomInfo { get; set; }
        public string HotelName { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime DueDate { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Tax { get; set; }
        public decimal Total { get; set; }
        public bool IsPaid { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime? PaymentDate { get; set; }
    }
}