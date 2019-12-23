using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace ThAmCo.Events.Models.Availability
{
    public class AvailabilitiesVM
    {
        
        public string Code { get; set; }

        [DisplayName("Venue Name")]
        public string Name { get; set; }

        [DisplayName("Venue Description")]
        public string Description { get; set; }

        public int Capacity { get; set; }

        public DateTime Date { get; set; }

        [DisplayName("Cost per hour")]
        public double CostPerHour { get; set; }
    }
}
