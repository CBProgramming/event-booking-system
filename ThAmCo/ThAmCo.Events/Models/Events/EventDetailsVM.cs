using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using ThAmCo.Events.Models.Customers;
using ThAmCo.Events.Models.Staff;
using ThAmCo.Events.Services;

namespace ThAmCo.Events.Models.Events
{
    public class EventDetailsVM
    {
        public EventDetailsVM(EventVM @event, List<CustomerVM> customers, List<StaffVM> staff, MenuDto menu)
        {
            Event = @event;
            Customers = customers;
            Staff = staff;
            Menu = menu;
        }

        public EventVM Event { get; set; }

        public List<CustomerVM> Customers { get; set; }

        public List<StaffVM> Staff { get; set; }

        public MenuDto Menu { get; set; }

        [DisplayName("Menu Cost")]
        public double MenuCost
        {
            get { return Menu.CostPerHead * Customers.Count; }
        }

        [DisplayName("Venue Cost")]
        public double VenueCost
        {
            get { return Event.VenueCost * ((TimeSpan)Event.Duration).TotalHours; }
        }

        [DisplayName("Total Cost")]
        public double TotalCost         
        {
            get { return MenuCost + VenueCost; }
        }
    }
}
