using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            IsActive = eventData.IsActive;
        }

        public EventVM(int id, string title, DateTime date, TimeSpan duration, string typeId, string venueName, string venueDescription, int venueCapacity, double venueCost, bool existing, string venueRef, string oldRef)
        {
            Id = id;
            Title = title;
            Date = date;
            Duration = duration;
            TypeId = typeId;
            VenueName = venueName;
            VenueDescription = venueDescription;
            VenueCapacity = venueCapacity;
            VenueCost = venueCost;
            Existing = existing;
            VenueRef = venueRef;
            OldRef = oldRef;
        }

        public EventVM(Event eventData, bool existing)
        {
            Id = eventData.Id;
            Title = eventData.Title;
            Date = eventData.Date;
            Duration = eventData.Duration;
            TypeId = eventData.TypeId;
            Bookings = eventData.Bookings;
            VenueRef = eventData.VenueRef;
            VenueName = eventData.VenueName;
            VenueDescription = eventData.VenueDescription;
            VenueCapacity = eventData.VenueCapacity;
            VenueCost = eventData.VenueCost;
            Existing = existing;
            IsActive = eventData.IsActive;
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
            VenueName = booking.VenueName;
            VenueDescription = booking.VenueDescription;
            VenueCapacity = booking.VenueCapacity;
            VenueCost = booking.VenueCost;
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime Date { get; set; }

        public TimeSpan? Duration { get; set; }

        public string TypeId { get; set; }

        public List<GuestBooking> Bookings { get; set; }

        public string VenueRef { get; set; }

        public string VenueName { get; set; }

        public string VenueDescription { get; set; }

        public int VenueCapacity { get; set; }

        public double VenueCost { get; set; }

        public string Message { get; set; }

        public bool Existing { get; set; }

        public string OldRef { get; set; }

        public string getBookingRef
        {
            get
            {
                return VenueRef + Date.Year.ToString("0000") + Date.Month.ToString("00") + Date.Day.ToString("00");
            }
        }

        public int NumGuests { get; set; }

        [DefaultValue(true)]
        public bool IsActive { get; set; }
    }
}
