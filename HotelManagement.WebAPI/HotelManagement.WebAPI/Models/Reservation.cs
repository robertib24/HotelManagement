using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HotelManagement.WebAPI.Models;
using Unity;

namespace HotelManagement.WebAPI.Models
{
    public class Reservation
    {
        public Reservation()
        {
            ReservationServices = new HashSet<ReservationService>();
        }

        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int RoomId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int NumberOfGuests { get; set; }
        public decimal TotalPrice { get; set; }
        public string ReservationStatus { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Notes { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Room Room { get; set; }
        public virtual ICollection<ReservationService> ReservationServices { get; set; }
        public virtual Invoice Invoice { get; set; }
    }
}