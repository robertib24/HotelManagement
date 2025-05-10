using System;

namespace HotelManagement.Client.Models
{
    public class RoomTypeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int MaxOccupancy { get; set; }
        public decimal BasePrice { get; set; }
        public int AvailableRooms { get; set; }
    }
}