using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThAmCo.Events.Data;

namespace ThAmCo.Events.Models.Events
{
    public class EventVM
    {
        public EventVM()
        { }

        public EventVM(Event eventData)
        {
            Id = eventData.Id;
            Title = eventData.Title;
            Date = eventData.Date;
            Duration = eventData.Duration;
            TypeId = eventData.TypeId;
            Bookings = eventData.Bookings;
            VenueRef = eventData.VenueRef;
            VenueName = eventData.VenueName;
        }

        public EventVM(string title, DateTime date, TimeSpan duration, string typeId)
        {
            Title = title;
            Date = date;
            Duration = duration;
            TypeId = typeId;
            VenueRef = "No venue allocated";
        }

        public EventVM(FinalBookingVM booking)
        {
            Title = booking.Title;
            Date = booking.Date;
            Duration = booking.Duration;
            TypeId = booking.TypeId;
            VenueRef = booking.VenueRef;
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime Date { get; set; }

        public TimeSpan? Duration { get; set; }

        public string TypeId { get; set; }

        public List<GuestBooking> Bookings { get; set; }

        public string VenueRef { get; set; }

        public string VenueName { get; set; }

        public string Message { get; set; }
    }
}
