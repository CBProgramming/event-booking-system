using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThAmCo.Events.Data;

namespace ThAmCo.Events.Models.Events
{
    public class CustomerBookingVM
    {
        public CustomerBookingVM(Customer c,GuestBooking b)
        {
            Id = c.Id;
            FullName = c.FullName;
            Attended = b.Attended;
        }

        public int Id { get; set; }

        public string FullName { get; set; }

        public bool Attended { get; set; }
    }
}
