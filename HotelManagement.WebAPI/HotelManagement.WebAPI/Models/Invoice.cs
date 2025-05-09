using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HotelManagement.WebAPI.Models;

namespace HotelManagement.WebAPI.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public int ReservationId { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime DueDate { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Tax { get; set; }
        public decimal Total { get; set; }
        public bool IsPaid { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime? PaymentDate { get; set; }

        public virtual Reservation Reservation { get; set; }
    }
}