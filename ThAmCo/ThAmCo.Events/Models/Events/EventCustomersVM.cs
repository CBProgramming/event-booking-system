using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThAmCo.Events.Data;
using ThAmCo.Events.Models.Events;

namespace ThAmCo.Events.Models.Events
{
    public class EventCustomersVM
    {
        public EventCustomersVM(Event @event, List<CustomerBookingVM> customers)
        {
            Event = @event;
            Customers = customers;
        }

        public Event Event { get; set; }

        public List<CustomerBookingVM> Customers { get; set; }

    }
}
