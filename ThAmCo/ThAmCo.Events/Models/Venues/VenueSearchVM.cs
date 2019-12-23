using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThAmCo.Events.Models.Venues
{
    public class VenueSearchVM
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string TypeId { get; set; }

        public string Type { get; set; }

        public string Message { get; set; }

        public SelectList TypeList { get; set; }
    }
}
