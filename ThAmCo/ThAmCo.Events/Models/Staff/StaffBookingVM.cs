using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThAmCo.Events.Data;
using ThAmCo.Events.Models.Customers;
using ThAmCo.Events.Models.Events;

namespace ThAmCo.Events.Models.GuestBookings
{
    public class StaffBookingVM
    { 
        public int StaffId { get; set; }

        public int EventId { get; set; }

        public bool Attended { get; set; }
    }
}
