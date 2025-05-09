using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelManagement.WebAPI.DTOs
{
    public class RoomDto
    {
        public int Id { get; set; }
        public string RoomNumber { get; set; }
        public int HotelId { get; set; }
        public string HotelName { get; set; }
        public int RoomTypeId { get; set; }
        public string RoomTypeName { get; set; }
        public int Floor { get; set; }
        public bool IsClean { get; set; }
        public bool IsOccupied { get; set; }
        public bool NeedsRepair { get; set; }
        public string Notes { get; set; }
        public decimal Price { get; set; }
    }
}