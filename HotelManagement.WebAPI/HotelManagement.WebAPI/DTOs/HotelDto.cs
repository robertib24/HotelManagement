using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelManagement.WebAPI.DTOs
{
    public class HotelDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public int Stars { get; set; }
        public string ContactPhone { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public int AvailableRooms { get; set; }
        public int TotalRooms { get; set; }
    }
}