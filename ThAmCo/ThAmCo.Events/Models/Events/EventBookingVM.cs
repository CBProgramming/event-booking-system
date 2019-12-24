using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using ThAmCo.Events.Data;

namespace ThAmCo.Events.Models.Events
{
    public class EventBookingVM
    {
        public EventBookingVM(Event e,GuestBooking b)
        {
            EventId = e.Id;
            Title = e.Title;
            Date = e.Date;
            Duration = e.Duration;
            TypeId = e.TypeId;
            Attended = b.Attended;
            Type = e.Type;
        }

        public int EventId { get; set; }

        [DisplayName("Event Name")]
        public string Title { get; set; }

        public DateTime Date { get; set; }

        public TimeSpan? Duration { get; set; }

        public string TypeId { get; set; }

        public string Type { get; set; }

        public bool Attended { get; set; }
    }
}
