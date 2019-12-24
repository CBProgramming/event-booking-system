using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThAmCo.Events.Models.Customers;
using ThAmCo.Events.Models.Events;

namespace ThAmCo.Events.Models.GuestBookings
{
    public class BookNewGuestVM
    {
        public BookNewGuestVM(EventVM @event, List<CustomerVM> customers)
        {
            Customers = customers;
            Event = @event;
        }

        public List<CustomerVM> Customers { get; set; }

        public EventVM Event { get; set; }
    }
}

