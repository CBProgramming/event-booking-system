using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThAmCo.Catering.Data
{
    public class Menu
    {
        public int MenuId { get; set; }

        public string Name { get; set; }

        public double CostPerHead { get; set; }

        public string Starter { get; set; }

        public string Main { get; set; }

        public string Dessert { get; set; }

        public List<FoodBooking> Bookings { get; set; }
    }
}
