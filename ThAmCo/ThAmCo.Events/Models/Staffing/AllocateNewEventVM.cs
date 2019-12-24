using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThAmCo.Events.Models.Events;
using ThAmCo.Events.Models.Staff;

namespace ThAmCo.Events.Models.Staffing
{
    public class AllocateNewEventVM
    {
        public AllocateNewEventVM(StaffVM staff, List<EventVM> venues)
        {
            Staff = staff;
            Venues = venues;
        }

        public StaffVM Staff { get; set; }

        public List<EventVM> Venues { get; set; }

        public bool Allocated { get; set; }
    }
}
