using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ThAmCo.Events.Data
{
    public class Event
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        [Required]
        public DateTime Date { get; set; }

        public TimeSpan? Duration { get; set; }

        [Required, MaxLength(3), MinLength(3)]
        public string TypeId { get; set; }

        public List<GuestBooking> Bookings { get; set; }

        public List<Staffing> Staffing { get; set; }

        public string VenueRef { get; set; }

        public string VenueName { get; set; }

        public string VenueDescription { get; set; }

        public int VenueCapacity { get; set; }

        public double VenueCost { get; set; }
    }
}