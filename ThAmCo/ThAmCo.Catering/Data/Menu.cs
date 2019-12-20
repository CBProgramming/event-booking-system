using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ThAmCo.Catering.Data
{
    public class Menu
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public double CostPerHead { get; set; }

        public string Starter { get; set; }

        public string Main { get; set; }

        public string Dessert { get; set; }

        public List<FoodBooking> Bookings { get; set; }
    }
}
