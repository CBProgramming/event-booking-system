using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThAmCo.Events.Models.Availability;
using ThAmCo.Events.Models.Events;
using ThAmCo.Events.Services;

namespace ThAmCo.Events.Models.Venues
{
    public class EventVenueAvailabilityVM
    {
        public EventVenueAvailabilityVM()
        { }

        public EventVenueAvailabilityVM(EventVM @event, List<AvailabilitiesVM> venues)
        {
            Event = @event;
            Venues = venues;
        }

        public EventVM Event { get; set; }

        public List<AvailabilitiesVM> Venues { get; set; }
    }
}
