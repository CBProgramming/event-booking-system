using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThAmCo.Events.Models.Staff;

namespace ThAmCo.Events.Models.Events
{
    public class EventStaffVM
    {
        public EventStaffVM(List<StaffVM> staff, bool firstAiderPresent, int eventId, string eventTitle)
        {
            Staff = staff;
            FirstAiderPresent = firstAiderPresent;
            EventId = eventId;
            EventTitle = eventTitle;
        }

        public int EventId { get; set; }

        public string EventTitle { get; set; }

        public List<StaffVM> Staff { get; set; }

        public bool FirstAiderPresent { get; set; }
    }
}
