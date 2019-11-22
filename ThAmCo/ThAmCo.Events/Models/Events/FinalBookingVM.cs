using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThAmCo.Events.Models.Events
{
    public class FinalBookingVM
    {
        public FinalBookingVM()
        { }

        public string Code { get; set; }

        public DateTime Date { get; set; }

        public string VenueName { get; set; }

        public string Title{ get; set; }

        public TimeSpan Duration { get; set; }

        public string TypeId { get; set; }

        public string VenueRef
        {
            get
            {
                return Code + Date.Year.ToString("0000") + Date.Month.ToString("00") + Date.Day.ToString("00");
            }
        }


    }
}
