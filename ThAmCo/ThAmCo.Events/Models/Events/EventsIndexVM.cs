using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThAmCo.Events.Models.Events
{
    public class EventsIndexVM
    {
        public EventsIndexVM(List<EventVM> eventsOk, List<EventVM> eventsNeedStaff, List<EventVM> eventsNeedFirstAid)
        {
            EventsOk = eventsOk;
            EventsNeedStaff = eventsNeedStaff;
            EventsNeedFirstAid = eventsNeedFirstAid;
        }

        public List<EventVM> EventsOk { get; set; }

        public List<EventVM> EventsNeedStaff { get; set; }

        public List<EventVM> EventsNeedFirstAid { get; set; }
    }
}
