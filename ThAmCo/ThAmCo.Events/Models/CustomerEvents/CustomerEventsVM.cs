using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThAmCo.Events.Data;
using ThAmCo.Events.Models.Events;

namespace ThAmCo.Events.Models.CustomerEvents
{
    public class CustomerEventsVM
    {
        public CustomerEventsVM(Customer customer, List<EventBookingVM> events)
        {
            Customer = customer;
            Events = events;
        }

        public Customer Customer { get; set; }

        public List<EventBookingVM> Events { get; set; }

    }
}
