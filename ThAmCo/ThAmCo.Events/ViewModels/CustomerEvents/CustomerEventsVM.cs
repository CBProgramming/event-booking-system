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
        public CustomerEventsVM(Customer customer, List<Data.GuestBooking> events)
        {
            Customer = customer;
            Events = events;
        }

        public Customer Customer { get; set; }

        public List<Data.GuestBooking> Events { get; set; }
    }
}
