using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThAmCo.Events.Data;
using ThAmCo.Events.Models.Customers;
using ThAmCo.Events.Models.Events;

namespace ThAmCo.Events.Models.GuestBookings
{
    public class GuestBookingVM
    { 
        public GuestBookingVM(GuestBooking booking, CustomerVM customer, EventVM @event)
        {
            CustomerId = booking.CustomerId;
            Customer = customer;
            EventId = booking.EventId;
            Event = @event;
            Attended = booking.Attended;
        }

        public int CustomerId { get; set; }

        public CustomerVM Customer { get; set; }

        public int EventId { get; set; }

        public EventVM Event { get; set; }

        public bool Attended { get; set; }

        public string Origin { get; set; }
    }
}
