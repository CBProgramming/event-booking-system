using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThAmCo.Events.Data;

namespace ThAmCo.Events.ViewModels.CustomerEvents
{
    public class CustomerEventsVM
    {
        public CustomerEventsVM(Customer customer, IEnumerable<Data.Event> events, IEnumerable<Data.GuestBooking> bookings)
        {
            Customer = customer;
            Events = events;
            Bookings = bookings;
        }

        public Customer Customer { get; set; }

        public IEnumerable<Data.Event> Events { get; set; }

        public IEnumerable<Data.GuestBooking> Bookings { get; set; }

    }
}
