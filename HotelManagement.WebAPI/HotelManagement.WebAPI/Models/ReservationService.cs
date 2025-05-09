using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HotelManagement.WebAPI.Models;

namespace HotelManagement.WebAPI.Models
{
    public class ReservationService
    {
        public int Id { get; set; }
        public int ReservationId { get; set; }
        public int ServiceId { get; set; }
        public DateTime Date { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public string Notes { get; set; }

        public virtual Reservation Reservation { get; set; }
        public virtual Service Service { get; set; }
    }
}