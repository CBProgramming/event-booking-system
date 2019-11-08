using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThAmCo.Events.Data;

namespace ThAmCo.Events.ViewModels.GuestBookings
{
    public class GuestBookingVM
    { 
        public GuestBookingVM(Data.GuestBooking booking)
        {
            CustomerId = booking.CustomerId;
            Customer = booking.Customer;
            EventId = booking.EventId;
            Event = booking.Event;
            Attended = booking.Attended;
        }

        public int CustomerId { get; set; }

        public Customer Customer { get; set; }

        public int EventId { get; set; }

        public Event Event { get; set; }

        public bool Attended { get; set; }
    }
}
