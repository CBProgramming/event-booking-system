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

        public EventVenueVM(EventVM eventVM, string code, DateTime date, string venueName)
        {
            EventVM = eventVM;
            Code = code;
            Date = date;
            VenueName = venueName;
        }

        public EventVM EventVM { get; set; }

        public string Code { get; set; }

        public DateTime Date { get; set; }

        public string VenueName { get; set; }

        public string EventRef
        {
            get
            {
                return Code + Date.Year.ToString("0000") + Date.Month.ToString("00") + Date.Day.ToString("00");
            }
        }
    }
}
