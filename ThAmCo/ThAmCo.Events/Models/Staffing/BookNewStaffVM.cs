using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThAmCo.Events.Models.Events;

namespace ThAmCo.Events.Models.Staffing
{
    public class BookNewStaffVM
    {
        public BookNewStaffVM(EventVM @event, SelectList staff)
        {
            Staff = staff;
            Event = @event;
        }

        public SelectList Staff { get; set; }

        public EventVM Event { get; set; }
    }
}
