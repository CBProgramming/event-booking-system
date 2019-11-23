using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThAmCo.Events.Models.Events
{
    public class EventToEditVM
    {
        public EventToEditVM()
        {

        }
        public EventToEditVM(int id, string title, DateTime date, TimeSpan? duration, string typeId, string venueRef, 
                             string venueName, string venueDescription, int venueCapacity, double venueCost)
        {
            Id = id;
            Title = title;
            Date = date;
            Duration = duration;
            TypeId = typeId;
            VenueRef = venueRef;
            VenueName = venueName;
            VenueDescription = venueDescription;
            VenueCapacity = venueCapacity;
            VenueCost = venueCost;
    }

        public EventToEditVM(EventVM @event)
        {
            Id = @event.Id;
            Title = @event.Title;
            Date = @event.Date;
            Duration = @event.Duration;
            TypeId = @event.TypeId;
            VenueRef = @event.VenueRef;
            VenueName = @event.VenueName;
            VenueDescription = @event.VenueDescription;
            VenueCapacity = @event.VenueCapacity;
            VenueCost = @event.VenueCost;
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime Date { get; set; }

        public TimeSpan? Duration { get; set; }

        public string TypeId { get; set; }

        public string VenueRef { get; set; }

        public string VenueName { get; set; }

        public string VenueDescription { get; set; }

        public int VenueCapacity { get; set; }

        public double VenueCost { get; set; }

        public string Message { get; set; }
    }
}
