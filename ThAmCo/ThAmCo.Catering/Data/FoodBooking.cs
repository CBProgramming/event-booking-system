using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThAmCo.Catering.Data
{
    public class FoodBooking
    {
        public int MenuId { get; set; }

        public Menu Menu { get; set; }

        public int EventId { get; set; }

    }
}
