﻿using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace ThAmCo.Events.Data
{
    public class EventsDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<GuestBooking> Guests { get; set; }
        public DbSet<Staff> Staff { get; set; }
        public DbSet<Staffing> Staffing { get; set; }

        private IHostingEnvironment HostEnv { get; }

        public EventsDbContext(DbContextOptions<EventsDbContext> options,
                               IHostingEnvironment env) : base(options)
        {
            HostEnv = env;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.HasDefaultSchema("thamco.events");

            builder.Entity<GuestBooking>()
                   .HasKey(b => new { b.CustomerId, b.EventId });

            builder.Entity<Customer>()
                   .HasMany(c => c.Bookings)
                   .WithOne(b => b.Customer)
                   .HasForeignKey(b => b.CustomerId);

            builder.Entity<Event>()
                   .HasMany(e => e.Bookings)
                   .WithOne(b => b.Event)
                   .HasForeignKey(b => b.EventId);

            builder.Entity<Event>()
                   .Property(e => e.TypeId)
                   .IsFixedLength();

            builder.Entity<Staffing>()
                   .HasKey(s => new { s.StaffId, s.EventId });

            builder.Entity<Staff>()
                   .HasMany(s => s.Staffing)
                   .WithOne(t => t.Staff)
                   .HasForeignKey(t => t.StaffId);

            builder.Entity<Event>()
                   .HasMany(e => e.Staffing)
                   .WithOne(b => b.Event)
                   .HasForeignKey(b => b.EventId);

            // seed data for debug / development testing
            if (HostEnv != null && HostEnv.IsDevelopment())
            {
                builder.Entity<Customer>().HasData(
                    new Customer { Id = 1, Surname = "Robertson", FirstName = "Robert", Email = "bob@example.com" },
                    new Customer { Id = 2, Surname = "Thornton", FirstName = "Betty", Email = "betty@example.com" },
                    new Customer { Id = 3, Surname = "Jellybeans", FirstName = "Jin", Email = "jin@example.com" }
                );

                builder.Entity<Event>().HasData(
                    new Event { Id = 1, Title = "Bob's Big 50", Date = new DateTime(2016, 4, 12), Duration = new TimeSpan(6, 0, 0), TypeId = "PTY",
                                VenueRef = "",VenueName = "Crackling Hall", VenueDescription = "", VenueCapacity = 150, VenueCost = 100.00 },
                    new Event { Id = 2, Title = "Best Wedding Yet", Date = new DateTime(2018, 12, 1), Duration = new TimeSpan(12, 0, 0), TypeId = "WED", 
                                VenueRef = "", VenueName = "Crackling Hall", VenueDescription = "", VenueCapacity = 150, VenueCost = 100.00 }
                );

                builder.Entity<GuestBooking>().HasData(
                    new GuestBooking { CustomerId = 1, EventId = 1, Attended = true },
                    new GuestBooking { CustomerId = 2, EventId = 1, Attended = false },
                    new GuestBooking { CustomerId = 1, EventId = 2, Attended = false },
                    new GuestBooking { CustomerId = 3, EventId = 2, Attended = false }
                );

                builder.Entity<Staff>().HasData(
                    new Data.Staff { Id = 1, FirstName = "Fred", Surname = "Frederickson", Email = "fred@example.com", FirstAider = true, IsActive = true },
                    new Data.Staff { Id = 2, FirstName = "Jenny", Surname = "Jenson", Email = "jenny@example.com", FirstAider = false, IsActive = true },
                    new Data.Staff { Id = 3, FirstName = "Simon", Surname = "Simonson", Email = "simon@example.com", FirstAider = false, IsActive = true },
                    new Data.Staff { Id = 4, FirstName = "Linda", Surname = "Lindason", Email = "linda@example.com", FirstAider = false, IsActive = true },
                    new Data.Staff { Id = 5, FirstName = "Tom", Surname = "Thompson", Email = "top@example.com", FirstAider = false, IsActive = true },
                    new Data.Staff { Id = 6, FirstName = "Rachel", Surname = "Rachelson", Email = "rachel@example.com", FirstAider = false, IsActive = true },
                    new Data.Staff { Id = 7, FirstName = "Mike", Surname = "Michaelson", Email = "michael@example.com", FirstAider = false, IsActive = true }
                    );

                builder.Entity<Staffing>().HasData(
                    new Data.Staffing { EventId = 1, StaffId = 1 },
                    new Data.Staffing { EventId = 1, StaffId = 2 },
                    new Data.Staffing { EventId = 1, StaffId = 3 },
                    new Data.Staffing { EventId = 2, StaffId = 4 },
                    new Data.Staffing { EventId = 2, StaffId = 5 }
                    );
            }
        }
    }
}
