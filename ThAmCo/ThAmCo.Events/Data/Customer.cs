﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ThAmCo.Events.Data
{
    public class Customer
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

        public List<GuestBooking> Bookings { get; set; }

        public bool Deleted { get; set; }
    }
}
