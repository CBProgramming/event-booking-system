using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace ThAmCo.Events.Models.Events
{
    public class EventVenueVM
    {
        EventVenueVM()
        { }

        public EventVenueVM(EventVM eventVM, string venueRef, DateTime date, string venueName)
        {
            EventVM = eventVM;
            VenueRef = venueRef;
            Date = date;
            VenueName = venueName;
        }

        public EventVM EventVM { get; set; }

        [DisplayName("Venue Reference")]
        public string VenueRef { get; set; }

        public DateTime Date { get; set; }

        [DisplayName("Venue Name")]
        public string VenueName { get; set; }

        public string EventRef
        {
            get
            {
                return VenueRef + Date.Year.ToString("0000") + Date.Month.ToString("00") + Date.Day.ToString("00");
            }
        }
    }
}
