using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using ThAmCo.Events.Services;

namespace ThAmCo.Events.Models.Events
{
    public class EventNameAndMenuVM
    {
        public EventNameAndMenuVM()
        {

        }

        public EventNameAndMenuVM(EventVM @event, MenuDto menu)
        {
            EventId = @event.Id;
            MenuId = menu.MenuId;
            EventName = @event.Title;
            MenuName = menu.Name;
            Starter = menu.Starter;
            Main = menu.Main;
            Dessert = menu.Dessert;
            CostPerHead = menu.CostPerHead;
        }

        public int EventId { get; set; }

        public int MenuId { get; set; }

        [DisplayName("Event name")]
        public string EventName { get; set; }

        [DisplayName("Menu Name")]
        public string MenuName { get; set; }

        public string Starter { get; set; }

        public string Main { get; set; }

        public string Dessert { get; set; }

        [DisplayName("Menu Cost Per Guest")]
        public double CostPerHead { get; set; }
    }
}
