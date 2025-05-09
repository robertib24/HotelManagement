using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelManagement.WebAPI.Models
{
    public class Customer
    {
        public Customer()
        {
            Reservations = new HashSet<Reservation>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string IdNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime RegistrationDate { get; set; }
        public bool IsVIP { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}