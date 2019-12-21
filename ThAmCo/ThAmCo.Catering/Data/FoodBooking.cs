using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThAmCo.Catering.Data
{
    public class FoodBooking
    {
        public FoodBooking()
        {

        }

        public FoodBooking(int eventId, int menuId)
        {
            MenuId = menuId;
            EventId = eventId;
        }

        public FoodBooking(FoodBookingDto bookingDto)
        {
            MenuId = bookingDto.MenuId;
            EventId = bookingDto.EventId;
        }

        public int MenuId { get; set; }

        public Menu Menu { get; set; }

        public int EventId { get; set; }

    }
}
