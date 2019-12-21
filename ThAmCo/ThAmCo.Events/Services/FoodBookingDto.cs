using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThAmCo.Catering.Data
{
    public class FoodBookingDto
    {
        public FoodBookingDto(int menuId, int eventId)
        {
            MenuId = menuId;
            EventId = eventId;
        }

        public int MenuId { get; set; }

        public int EventId { get; set; }

    }
}
