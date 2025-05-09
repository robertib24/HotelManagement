using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelManagement.WebAPI.DTOs
{
    public class ReservationDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int RoomId { get; set; }
        public string RoomNumber { get; set; }
        public int HotelId { get; set; }
        public string HotelName { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int NumberOfGuests { get; set; }
        public decimal TotalPrice { get; set; }
        public string ReservationStatus { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Notes { get; set; }
        public List<ServiceDto> Services { get; set; }
        public bool IsPaid { get; set; }
        public int NightsStay { get { return (CheckOutDate - CheckInDate).Days; } }
    }
}