using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThAmCo.Events.Models.Customers;
using ThAmCo.Events.Models.Events;

namespace ThAmCo.Events.Models.GuestBookings
{
    public class GuestBookingCreateVM
    {
        public GuestBookingCreateVM (CustomerVM customer, List<EventVM> venues)
        {
            Customer = customer;
            Venues = venues;
        }

        public CustomerVM Customer { get; set; }

        public List<EventVM> Venues { get; set; }

    }
}
