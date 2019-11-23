using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ThAmCo.Events.Services
{
    public class ReservationGetDto
    {

        public ReservationGetDto()
        {

        }
        public string Reference { get; set; }

        public DateTime EventDate { get; set; }

        public string VenueCode { get; set; }

        public DateTime WhenMade { get; set; }

        public string StaffId { get; set; }

    }
}
