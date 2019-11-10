using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThAmCo.Events.Data;

namespace ThAmCo.Events.ViewModels.Events
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
        }

        public int EventId { get; set; }
        public string Title { get; set; }

        public DateTime Date { get; set; }

        public TimeSpan? Duration { get; set; }

        public string TypeId { get; set; }

        public bool Attended { get; set; }
    }
}
