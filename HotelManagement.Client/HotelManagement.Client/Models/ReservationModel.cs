using System;
using System.Collections.Generic;

namespace HotelManagement.Client.Models
{
    public class ReservationModel
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
        public List<ServiceModel> Services { get; set; }
        public bool IsPaid { get; set; }
        public int NightsStay => (CheckOutDate - CheckInDate).Days;

        public string StatusDisplay
        {
            get
            {
                switch (ReservationStatus)
                {
                    case "Confirmed": return "Confirmată";
                    case "CheckedIn": return "Check-In efectuat";
                    case "Completed": return "Finalizată";
                    case "Cancelled": return "Anulată";
                    default: return ReservationStatus;
                }
            }
        }
    }
}