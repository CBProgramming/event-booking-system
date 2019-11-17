using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThAmCo.Events.Models.Events
{
    public class EventVenueVM
    {
        EventVenueVM()
        { }

        public EventVenueVM(EventVM eventVM, string code, DateTime date)
        {
            EventVM = eventVM;
            Code = code;
            Date = date;
        }

        public EventVM EventVM { get; set; }

        public string Code { get; set; }

        public DateTime Date { get; set; }
    }
}
