using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThAmCo.Events.Models.GuestBookings
{
    public class GuestBookingAttendanceVM
    {
        public GuestBookingAttendanceVM()
        {

        }

        public GuestBookingAttendanceVM(int customerId, int eventId, bool attended)
        {
            CustomerId = customerId;
            EventId = eventId;
            Attended = attended;
        }

        public int CustomerId { get; set; }

        public int EventId { get; set; }

        public bool Attended { get; set; }
    }
}
