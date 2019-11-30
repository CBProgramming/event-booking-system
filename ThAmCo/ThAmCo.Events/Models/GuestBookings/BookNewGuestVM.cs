using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThAmCo.Events.Models.Events;

namespace ThAmCo.Events.Models.GuestBookings
{
    public class BookNewGuestVM
    {
        public BookNewGuestVM(EventVM @event, SelectList customers)
        {
            Customers = customers;
            Event = @event;
        }

        public SelectList Customers { get; set; }

        public EventVM Event { get; set; }
    }
}

