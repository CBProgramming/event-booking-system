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
            MenuNumber = menuId;
            EventId = eventId;
        }

        public FoodBooking(FoodBookingDto bookingDto)
        {
            MenuNumber = bookingDto.MenuId;
            EventId = bookingDto.EventId;
        }
        public int EventId { get; set; }

        public int MenuNumber { get; set; }

        public Menu Menu { get; set; }



    }
}
