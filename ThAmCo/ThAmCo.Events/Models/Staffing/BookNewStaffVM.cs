using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using ThAmCo.Events.Models.Events;
using ThAmCo.Events.Models.Staff;

namespace ThAmCo.Events.Models.Staffing
{
    public class BookNewStaffVM
    {
        public BookNewStaffVM(List<StaffAttendanceVM> staff, bool firstAiderPresent, int eventId, string eventTitle, int numGuests)
        {
            Staff = staff;
            FirstAiderPresent = firstAiderPresent;
            EventId = eventId;
            EventTitle = eventTitle;
            NumGuests = numGuests;
        }

        public List<StaffAttendanceVM> Staff { get; set; }

        public int EventId { get; set; }

        [DisplayName("Event Name")]
        public string EventTitle { get; set; }

        public bool FirstAiderPresent { get; set; }

        [DisplayName("Number of Guests")]
        public int NumGuests { get; set; }

        public int NeededStaff
        {
            get
            {
                return ((NumGuests + 9) / 10) - Staff.Count;
            }
        }
    }
}
