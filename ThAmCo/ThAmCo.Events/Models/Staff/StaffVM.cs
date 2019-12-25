using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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

        public StaffVM(Data.Staff staff, string message)
        {
            Id = staff.Id;
            Surname = staff.Surname;
            FirstName = staff.FirstName;
            Email = staff.Email;
            FirstAider = staff.FirstAider;
            Message = message;
        }

        public int Id { get; set; }

        [DisplayName("Full Name")]
        public string FullName
        {
            get { return FirstName + " " + Surname; }
        }

        [Required]
        public string Surname { get; set; }

        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DisplayName("First Aider")]
        public bool FirstAider { get; set; }

        public bool IsActive { get; set; }

        public List<ThAmCo.Events.Data.Staffing> Staffing { get; set; }

        public string Message { get; set; }
    }
}
