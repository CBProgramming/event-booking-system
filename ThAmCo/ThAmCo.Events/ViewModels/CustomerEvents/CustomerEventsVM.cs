using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThAmCo.Events.Data;
using ThAmCo.Events.ViewModels.Events;

namespace ThAmCo.Events.ViewModels.CustomerEvents
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
