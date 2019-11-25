using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThAmCo.Events.Data;

namespace ThAmCo.Events.Models.Staff
{
    public class StaffVM
    {
        public int Id { get; set; }

        public string FullName
        {
            get { return FirstName + " " + Surname; }
        }

        public string Surname { get; set; }

        public string FirstName { get; set; }

        public string Email { get; set; }

        public bool FirstAider { get; set; }

        public bool IsActive { get; set; }

        public List<ThAmCo.Events.Data.Staffing> Staffing { get; set; }
    }
}
