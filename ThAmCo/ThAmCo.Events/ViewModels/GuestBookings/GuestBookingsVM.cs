using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThAmCo.Events.ViewModels.GuestBookings
{
    public class GuestBookingsVM
    {
        public GuestBookingsVM(List<Data.GuestBooking> bookings)
        {
            Bookings = bookings;
        }

        public List<Data.GuestBooking> Bookings { get; set; }
    }
}
