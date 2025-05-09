using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HotelManagement.WebAPI.Models;

namespace HotelManagement.WebAPI.Models
{
    public class Room
    {
        public Room()
        {
            Reservations = new HashSet<Reservation>();
        }

        public int Id { get; set; }
        public string RoomNumber { get; set; }
        public int HotelId { get; set; }
        public int RoomTypeId { get; set; }
        public int Floor { get; set; }
        public bool IsClean { get; set; }
        public bool IsOccupied { get; set; }
        public bool NeedsRepair { get; set; }
        public string Notes { get; set; }

        public virtual Hotel Hotel { get; set; }
        public virtual RoomType RoomType { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}