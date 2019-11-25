using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThAmCo.Events.Data;

namespace ThAmCo.Events.Models.Staffing
{
    public class StaffingVM
    {
        public int StaffId { get; set; }

        public ThAmCo.Events.Data.Staff Staff { get; set; }

        public int EventId { get; set; }

        public Event Event { get; set; }
    }
}
