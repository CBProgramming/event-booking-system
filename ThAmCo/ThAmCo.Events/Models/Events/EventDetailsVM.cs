using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThAmCo.Events.Models.Customers;
using ThAmCo.Events.Models.Staff;

namespace ThAmCo.Events.Models.Events
{
    public class EventDetailsVM
    {
        public EventDetailsVM(EventVM @event, List<CustomerVM> customers, List<StaffVM> staff)
        {
            Event = @event;
            Customers = customers;
            Staff = staff;
        }

        public EventVM Event { get; set; }

        public List<CustomerVM> Customers { get; set; }

        public List<StaffVM> Staff { get; set; }
    }
}
