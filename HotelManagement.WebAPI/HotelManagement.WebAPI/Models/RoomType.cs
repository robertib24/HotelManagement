using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelManagement.WebAPI.Models
{
    public class RoomType
    {
        public RoomType()
        {
            Rooms = new HashSet<Room>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int MaxOccupancy { get; set; }
        public decimal BasePrice { get; set; }

        public virtual ICollection<Room> Rooms { get; set; }
    }
}