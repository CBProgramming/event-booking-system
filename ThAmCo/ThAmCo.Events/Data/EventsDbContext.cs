using System;
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
                    new Customer { Id = 1, Surname = "Robertson", FirstName = "Robert", Email = "bob@example.com", Deleted = false },
                    new Customer { Id = 2, Surname = "Thornton", FirstName = "Betty", Email = "betty@example.com", Deleted = false },
                    new Customer { Id = 3, Surname = "Jellybeans", FirstName = "Jin", Email = "jin@example.com", Deleted = false },
                    new Customer { Id = 4, Surname = "Garethson", FirstName = "Gareth", Email = "Gareth@example.com", Deleted = false },
                    new Customer { Id = 5, Surname = "Aliceson", FirstName = "Alice", Email = "Alice@example.com", Deleted = false },
                    new Customer { Id = 6, Surname = "Markson", FirstName = "Mark", Email = "Mark@example.com", Deleted = false },
                    new Customer { Id = 7, Surname = "Adamson", FirstName = "Adam", Email = "Adam@example.com", Deleted = false },
                    new Customer { Id = 8, Surname = "Charlotteson", FirstName = "Charlotte", Email = "Charlotte@example.com", Deleted = false },
                    new Customer { Id = 9, Surname = "Dianeson", FirstName = "Diane", Email = "Diane@example.com", Deleted = false },
                    new Customer { Id = 10, Surname = "Alanson", FirstName = "Alan", Email = "Alan@example.com", Deleted = false },
                    new Customer { Id = 11, Surname = "Sarahson", FirstName = "Sarah", Email = "Sarah@example.com", Deleted = false },
                    new Customer { Id = 12, Surname = "Lukeson", FirstName = "Luke", Email = "Luke@example.com", Deleted = false },
                    new Customer { Id = 13, Surname = "Camilleson", FirstName = "Camille", Email = "Camille@example.com", Deleted = false },
                    new Customer { Id = 14, Surname = "Laurason", FirstName = "Laura", Email = "Laura@example.com", Deleted = false },
                    new Customer { Id = 15, Surname = "Aidanson", FirstName = "Aidan", Email = "Aidan@example.com", Deleted = false },
                    new Customer { Id = 16, Surname = "Laylason", FirstName = "Layla", Email = "Layla@example.com", Deleted = false },
                    new Customer { Id = 17, Surname = "Philson", FirstName = "Phil", Email = "Phil@example.com", Deleted = false },
                    new Customer { Id = 18, Surname = "Katson", FirstName = "Kat", Email = "Kat@example.com", Deleted = false },
                    new Customer { Id = 19, Surname = "Peterson", FirstName = "Peter", Email = "Peter@example.com", Deleted = false },
                    new Customer { Id = 20, Surname = "Valson", FirstName = "Val", Email = "Val@example.com", Deleted = false }
                );

                builder.Entity<Event>().HasData(
                    new Event { Id = 1, Title = "Bob's Big 50", Date = new DateTime(2016, 4, 12), Duration = new TimeSpan(6, 0, 0), TypeId = "PTY",
                                VenueRef = "",VenueName = "Crackling Hall", VenueDescription = "Once the residence of Lord and Lady Crackling, this lavish dwelling remains a prime example of 18th century fine living.", 
                                VenueCapacity = 150, VenueCost = 100.00, menuId = 1 },
                    new Event { Id = 2, Title = "Best Wedding Yet", Date = new DateTime(2018, 11, 1), Duration = new TimeSpan(12, 0, 0), TypeId = "WED", 
                                VenueRef = "", VenueName = "Crackling Hall", VenueDescription = "Once the residence of Lord and Lady Crackling, this lavish dwelling remains a prime example of 18th century fine living.",
                                VenueCapacity = 150, VenueCost = 100.00, menuId = 2},
                    new Event { Id = 3, Title = "Billie's Birthday Bonanza", Date = new DateTime(2018, 10, 1), Duration = new TimeSpan(12, 0, 0), TypeId = "PTY", 
                                VenueRef = "", VenueName = "Tinder Manor", VenueDescription = "Refurbished manor house with fully equipped facilities ready to help you have a good time in business or pleasure.", 
                                VenueCapacity = 150, VenueCost = 100.00, menuId = 3 },
                    new Event { Id = 4, Title = "Stevie's Stag", Date = new DateTime(2019, 12, 1), Duration = new TimeSpan(12, 0, 0), TypeId = "PYT", 
                                VenueRef = "", VenueName = "Tinder Manor", VenueDescription = "Refurbished manor house with fully equipped facilities ready to help you have a good time in business or pleasure.", 
                                VenueCapacity = 150, VenueCost = 100.00, menuId = 1 },
                    new Event { Id = 5, Title = "Cheryl and Fleur get hitched", Date = new DateTime(2019, 11, 1), Duration = new TimeSpan(12, 0, 0), TypeId = "WED", 
                                VenueRef = "", VenueName = "Tinder Manor", VenueDescription = "Refurbished manor house with fully equipped facilities ready to help you have a good time in business or pleasure.", 
                                VenueCapacity = 150, VenueCost = 100.00 },
                    new Event { Id = 6, Title = "George's Divorce Party", Date = new DateTime(2019, 8, 1), Duration = new TimeSpan(12, 0, 0), TypeId = "PTY", 
                                VenueRef = "", VenueName = "Tinder Manor", VenueDescription = "Refurbished manor house with fully equipped facilities ready to help you have a good time in business or pleasure.", 
                                 VenueCapacity = 150, VenueCost = 100.00 }
                );

                builder.Entity<GuestBooking>().HasData(
                    new GuestBooking { CustomerId = 1, EventId = 1, Attended = true },
                    new GuestBooking { CustomerId = 2, EventId = 1, Attended = false },
                    new GuestBooking { CustomerId = 3, EventId = 1, Attended = false },
                    new GuestBooking { CustomerId = 4, EventId = 1, Attended = false },
                    new GuestBooking { CustomerId = 5, EventId = 1, Attended = false },
                    new GuestBooking { CustomerId = 6, EventId = 1, Attended = false },
                    new GuestBooking { CustomerId = 7, EventId = 1, Attended = false },
                    new GuestBooking { CustomerId = 8, EventId = 1, Attended = false },
                    new GuestBooking { CustomerId = 9, EventId = 1, Attended = false },
                    new GuestBooking { CustomerId = 10, EventId = 1, Attended = false },
                    new GuestBooking { CustomerId = 11, EventId = 1, Attended = false },
                    new GuestBooking { CustomerId = 12, EventId = 1, Attended = false },
                    new GuestBooking { CustomerId = 13, EventId = 1, Attended = false },
                    new GuestBooking { CustomerId = 14, EventId = 1, Attended = false },
                    new GuestBooking { CustomerId = 15, EventId = 1, Attended = false },
                    new GuestBooking { CustomerId = 16, EventId = 1, Attended = false },
                    new GuestBooking { CustomerId = 17, EventId = 1, Attended = false },
                    new GuestBooking { CustomerId = 18, EventId = 1, Attended = false },
                    new GuestBooking { CustomerId = 19, EventId = 1, Attended = false },
                    new GuestBooking { CustomerId = 20, EventId = 4, Attended = false },
                    new GuestBooking { CustomerId = 1, EventId = 2, Attended = false },
                    new GuestBooking { CustomerId = 2, EventId = 2, Attended = false },
                    new GuestBooking { CustomerId = 3, EventId = 2, Attended = false },
                    new GuestBooking { CustomerId = 4, EventId = 2, Attended = false },
                    new GuestBooking { CustomerId = 5, EventId = 2, Attended = false },
                    new GuestBooking { CustomerId = 6, EventId = 2, Attended = false },
                    new GuestBooking { CustomerId = 7, EventId = 2, Attended = false },
                    new GuestBooking { CustomerId = 8, EventId = 2, Attended = false },
                    new GuestBooking { CustomerId = 9, EventId = 2, Attended = false },
                    new GuestBooking { CustomerId = 10, EventId = 3, Attended = false },
                    new GuestBooking { CustomerId = 11, EventId = 3, Attended = false },
                    new GuestBooking { CustomerId = 12, EventId = 3, Attended = false },
                    new GuestBooking { CustomerId = 13, EventId = 3, Attended = false },
                    new GuestBooking { CustomerId = 14, EventId = 3, Attended = false }
                );

                builder.Entity<Staff>().HasData(
                    new Data.Staff { Id = 1, FirstName = "Fred", Surname = "Frederickson", Email = "fred@example.com", FirstAider = true, IsActive = true },
                    new Data.Staff { Id = 2, FirstName = "Jenny", Surname = "Jenson", Email = "jenny@example.com", FirstAider = false, IsActive = true },
                    new Data.Staff { Id = 3, FirstName = "Simon", Surname = "Simonson", Email = "simon@example.com", FirstAider = false, IsActive = true },
                    new Data.Staff { Id = 4, FirstName = "Linda", Surname = "Lindason", Email = "linda@example.com", FirstAider = false, IsActive = true },
                    new Data.Staff { Id = 5, FirstName = "Tom", Surname = "Thompson", Email = "tom@example.com", FirstAider = false, IsActive = true },
                    new Data.Staff { Id = 6, FirstName = "Rachel", Surname = "Rachelson", Email = "rachel@example.com", FirstAider = false, IsActive = true },
                    new Data.Staff { Id = 7, FirstName = "Mike", Surname = "Michaelson", Email = "michael@example.com", FirstAider = false, IsActive = true }
                    );

                builder.Entity<Staffing>().HasData(
                    new Data.Staffing { EventId = 1, StaffId = 2 },
                    new Data.Staffing { EventId = 3, StaffId = 2 },
                    new Data.Staffing { EventId = 3, StaffId = 3 },
                    new Data.Staffing { EventId = 4, StaffId = 4 },
                    new Data.Staffing { EventId = 4, StaffId = 5 }
                    );
            }
        }
    }
}
