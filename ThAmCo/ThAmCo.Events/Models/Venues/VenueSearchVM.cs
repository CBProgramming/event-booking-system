using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace ThAmCo.Events.Models.Venues
{
    public class VenueSearchVM
    {
        [DisplayName("Start Date")]
        public DateTime StartDate { get; set; }

        [DisplayName("End Date")]
        public DateTime EndDate { get; set; }

        [DisplayName("Event Type")]
        public string TypeId { get; set; }

        public string Type { get; set; }

        public string Message { get; set; }

        public SelectList TypeList { get; set; }
    }
}
