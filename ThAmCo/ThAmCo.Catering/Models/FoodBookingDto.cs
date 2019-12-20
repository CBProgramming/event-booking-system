using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThAmCo.Catering.Data
{
    public class FoodBookingDto
    {

        public FoodBookingDto(FoodBooking booking)
        {
            MenuId = booking.MenuId;
            EventId = booking.EventId;
        }

        public int MenuId { get; set; }

        public int EventId { get; set; }

    }
}
