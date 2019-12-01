using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThAmCo.Events.Data;
using ThAmCo.Events.Models.Events;
using ThAmCo.Events.Models.Staff;

namespace ThAmCo.Events.Models.Staffing
{
    public class StaffingVM
    {
        public StaffingVM(Data.Staffing staffing, StaffVM staff, EventVM @event)
        {
            StaffId = staffing.StaffId;
            Staff = staff;
            EventId = staffing.EventId;
            Event = @event;
        }

        public StaffingVM(Data.Staffing staffing)
        {
            StaffId = staffing.StaffId;
            Staff = new StaffVM(staffing.Staff);
            EventId = staffing.EventId;
            Event = new EventVM(staffing.Event);
        }

        public int StaffId { get; set; }

        public StaffVM Staff { get; set; }

        public int EventId { get; set; }

        public EventVM Event { get; set; }
    }
}
