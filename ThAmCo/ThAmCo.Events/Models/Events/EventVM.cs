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

        public EventVM(Data.Event eventData)
        {
            Id = eventData.Id;
            Title = eventData.Title;
            Date = eventData.Date;
            Duration = eventData.Duration;
            TypeId = eventData.TypeId;
            Bookings = eventData.Bookings;
        }

        public EventVM(string title, DateTime date, TimeSpan duration, string typeId)
        {
            Title = title;
            Date = date;
            Duration = duration;
            TypeId = typeId;
        }

        public EventVM(FinalBookingVM booking)
        {
            Title = booking.Title;
            Date = booking.Date;
            Duration = booking.Duration;
            TypeId = booking.TypeId;
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime Date { get; set; }

        public TimeSpan? Duration { get; set; }

        public string TypeId { get; set; }

        public List<GuestBooking> Bookings { get; set; }

        public string Message { get; set; }
    }
}
