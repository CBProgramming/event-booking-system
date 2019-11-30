using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThAmCo.Events.Models.Events;

namespace ThAmCo.Events.Models.Staff
{
    public class StaffEventsVM
    {
        public StaffEventsVM(StaffVM staff, List<EventVM> events)
        {
            Staff = staff;
            Events = events;
        }

        public StaffVM Staff { get; set; }

        public List<EventVM> Events { get; set; }
    }
}
