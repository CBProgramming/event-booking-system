using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;


namespace ThAmCo.Events.Models.Staff
{
    public class StaffVM
    {
        public StaffVM()
        {

        }

        public StaffVM(Data.Staff staff)
        {
            Id = staff.Id;
            Surname = staff.Surname;
            FirstName = staff.FirstName;
            Email = staff.Email;
            FirstAider = staff.FirstAider;
        }

        public int Id { get; set; }

        [DisplayName("Full Name")]
        public string FullName
        {
            get { return FirstName + " " + Surname; }
        }

        public string Surname { get; set; }

        [DisplayName("First Name")]
        public string FirstName { get; set; }

        public string Email { get; set; }

        [DisplayName("First Aider")]
        public bool FirstAider { get; set; }

        public bool IsActive { get; set; }

        public List<ThAmCo.Events.Data.Staffing> Staffing { get; set; }
    }
}
