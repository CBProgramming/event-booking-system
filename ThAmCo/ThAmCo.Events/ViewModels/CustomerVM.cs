using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThAmCo.Events.Data;

namespace ThAmCo.Events.ViewModels
{
    public class CustomerVM
    {
        CustomerVM(int id, string firstName, string surname, string email, List<GuestBooking> bookings)
        {
            Id = id;
            FirstName = firstName;
            Surname = surname;
            Email = email;
            Bookings = bookings;
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
    }
}
