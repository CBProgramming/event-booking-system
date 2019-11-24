using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ThAmCo.Events.Data
{
    public class Staff
    {
        public int Id { get; set; }

        public string FullName
        {
            get { return FirstName + " " + Surname; }
        }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public bool FirstAider { get; set; }

        public bool IsActive { get; set; }

        public List<Staffing> Staffing { get; set; }
    }
}
