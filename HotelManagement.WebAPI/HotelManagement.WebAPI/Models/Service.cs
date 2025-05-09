using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelManagement.WebAPI.Models
{
    public class Service
    {
        public Service()
        {
            ReservationServices = new HashSet<ReservationService>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public bool IsAvailable { get; set; }

        public virtual ICollection<ReservationService> ReservationServices { get; set; }
    }
}