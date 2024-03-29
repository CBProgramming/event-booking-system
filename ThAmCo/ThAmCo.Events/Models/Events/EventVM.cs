﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ThAmCo.Events.Data;

namespace ThAmCo.Events.Models.Events
{
    public class EventVM
    {
        public EventVM()
        { }

        public EventVM(Event eventData)
        {
            Id = eventData.Id;
            Title = eventData.Title;
            Date = eventData.Date;
            Duration = eventData.Duration;
            TypeId = eventData.TypeId;
            Bookings = eventData.Bookings;
            VenueRef = eventData.VenueRef;
            VenueName = eventData.VenueName;
            VenueDescription = eventData.VenueDescription;
            VenueCapacity = eventData.VenueCapacity;
            VenueCost = eventData.VenueCost;
            IsActive = eventData.IsActive;
            MenuId = eventData.menuId;
            Type = eventData.Type;
        }

        public EventVM(Event eventData, bool existing)
        {
            Id = eventData.Id;
            Title = eventData.Title;
            Date = eventData.Date;
            Duration = eventData.Duration;
            TypeId = eventData.TypeId;
            Bookings = eventData.Bookings;
            VenueRef = eventData.VenueRef;
            VenueName = eventData.VenueName;
            VenueDescription = eventData.VenueDescription;
            VenueCapacity = eventData.VenueCapacity;
            VenueCost = eventData.VenueCost;
            Existing = existing;
            MenuId = eventData.menuId;
            IsActive = eventData.IsActive;
            Type = eventData.Type;
        }

        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public TimeSpan? Duration { get; set; }

        [DisplayName("Event type")]
        public string TypeId { get; set; }

        public List<GuestBooking> Bookings { get; set; }

        public string VenueRef { get; set; }

        [DisplayName("Venue Name")]
        public string VenueName { get; set; }

        [DisplayName("Venue Description")]
        public string VenueDescription { get; set; }

        [DisplayName("Venue Capacity")]
        public int VenueCapacity { get; set; }

        [DisplayName("Venue Cost")]
        public double VenueCost { get; set; }

        public int MenuId { get; set; }

        [DisplayName("Menu Name")]
        public string MenuName { get; set; }

        public string Message { get; set; }

        public bool Existing { get; set; }

        public string OldRef { get; set; }

        public string getBookingRef
        {
            get
            {
                return VenueRef + Date.Year.ToString("0000") + Date.Month.ToString("00") + Date.Day.ToString("00");
            }
        }

        [DisplayName("Number of Guests")]
        public int NumGuests { get; set; }

        [DisplayName("Number of Staff")]
        public int NumStaff { get; set; }

        public bool NeedStaff { get
            {
                return NumStaff < ((NumGuests + 9) / 10);
            }
        }

        [DefaultValue(true)]
        public bool IsActive { get; set; }

        [DisplayName("Event type")]
        public string Type { get; set; }

        public SelectList TypeList { get; set; }

        [DisplayName("Booked")]
        public bool Allocated { get; set; }

    }
}
