using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ThAmCo.Events.Data;

namespace ThAmCo.Events.Models.Customers
{
    public class CustomerVM
    {
        public CustomerVM()
        {

        }
        public CustomerVM(Customer customer)
        {
            Id = customer.Id;
            Surname = customer.Surname;
            FirstName = customer.FirstName;
            Email = customer.Email;
            Deleted = customer.Deleted;
            Bookings = customer.Bookings;
        }

        public int Id { get; set; }

        [DisplayName("Full Name")]
        public string FullName
        {
            get { return FirstName + " " + Surname; }
        }

        [Required]
        public string Surname { get; set; }

        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [Required]
        public string Email { get; set; }

        public List<GuestBooking> Bookings { get; set; }

        public bool Deleted { get; set; }

        public string Message { get; set; }

        public bool Booked { get; set; }
    }
}

