using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThAmCo.Events.Models.Events;
using ThAmCo.Events.Models.Staff;

namespace ThAmCo.Events.Models.Staffing
{
    public class BookNewStaffVM
    {
        public BookNewStaffVM(EventVM @event, List<StaffAttendanceVM> staff)
        {
            Staff = staff;
            Event = @event;
        }

        public List<StaffAttendanceVM> Staff { get; set; }

        public EventVM Event { get; set; }
    }
}
