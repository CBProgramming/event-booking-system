using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThAmCo.Events.Data;

namespace ThAmCo.Events.Models.Customers
{
    public class CustomerVM
    {
        public CustomerVM(Data.Customer customer)
        {
            Id = customer.Id;
            Surname = customer.Surname;
            FirstName = customer.FirstName;
            Email = customer.Email;
            Deleted = customer.Deleted;
            Bookings = customer.Bookings;
        }

        public int Id { get; set; }

        public string FullName
        {
            get { return FirstName + " " + Surname; }
        }

        public string Surname { get; set; }

        public string FirstName { get; set; }

        public string Email { get; set; }

        public List<GuestBooking> Bookings { get; set; }

        public bool Deleted { get; set; }
    }
}

