using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThAmCo.Events.ViewModels.Events
{
    public class EventsVM
    {
        public EventsVM(List<Data.Event> events)
        {
            Events = events;
        }

        public List<Data.Event> Events { get; set; }
    }
}
