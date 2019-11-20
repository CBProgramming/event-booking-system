using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ThAmCo.Events.Services
{
    public class ReservationPostDto 
    {
        public ReservationPostDto(DateTime eventDate, string venueCode, string staffId)
        {
            EventDate = eventDate;
            VenueCode = venueCode;
            StaffId = staffId;
        }

        public ReservationPostDto(DateTime eventDate, string venueCode)
        {
            EventDate = eventDate;
            VenueCode = venueCode;
            StaffId = "none";
        }

        [Required, DataType(DataType.Date)]
        public DateTime EventDate { get; set; }

        public string VenueCode { get; set; }

        public string StaffId { get; set; }
    }
}
