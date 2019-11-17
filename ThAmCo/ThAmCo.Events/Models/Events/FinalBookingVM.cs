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

        public string Title{ get; set; }

        public TimeSpan Duration { get; set; }

        public string TypeId { get; set; }
    }
}
