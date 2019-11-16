using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThAmCo.Events.Models.Customers;

namespace ThAmCo.Events.Models.GuestBookings
{
    public class GuestBookingCreateVM
    {
        public GuestBookingCreateVM (CustomerVM customer, SelectList venues)
        {
            Customer = customer;
            Venues = venues;
        }

        public CustomerVM Customer { get; set; }

        public SelectList Venues { get; set; }
    }
}
