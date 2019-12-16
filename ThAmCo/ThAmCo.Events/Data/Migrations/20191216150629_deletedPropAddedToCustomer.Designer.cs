﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ThAmCo.Events.Data;

namespace ThAmCo.Events.Data.Migrations
{
    [DbContext(typeof(EventsDbContext))]
    [Migration("20191216150629_deletedPropAddedToCustomer")]
    partial class deletedPropAddedToCustomer
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("thamco.events")
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ThAmCo.Events.Data.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Deleted");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("Surname")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Customers");

                    b.HasData(
                        new { Id = 1, Deleted = false, Email = "bob@example.com", FirstName = "Robert", Surname = "Robertson" },
                        new { Id = 2, Deleted = false, Email = "betty@example.com", FirstName = "Betty", Surname = "Thornton" },
                        new { Id = 3, Deleted = false, Email = "jin@example.com", FirstName = "Jin", Surname = "Jellybeans" },
                        new { Id = 4, Deleted = false, Email = "Gareth@example.com", FirstName = "Gareth", Surname = "Garethson" },
                        new { Id = 5, Deleted = false, Email = "Alice@example.com", FirstName = "Alice", Surname = "Aliceson" },
                        new { Id = 6, Deleted = false, Email = "Mark@example.com", FirstName = "Mark", Surname = "Markson" },
                        new { Id = 7, Deleted = false, Email = "Adam@example.com", FirstName = "Adam", Surname = "Adamson" },
                        new { Id = 8, Deleted = false, Email = "Charlotte@example.com", FirstName = "Charlotte", Surname = "Charlotteson" },
                        new { Id = 9, Deleted = false, Email = "Diane@example.com", FirstName = "Diane", Surname = "Dianeson" },
                        new { Id = 10, Deleted = false, Email = "Alan@example.com", FirstName = "Alan", Surname = "Alanson" },
                        new { Id = 11, Deleted = false, Email = "Sarah@example.com", FirstName = "Sarah", Surname = "Sarahson" },
                        new { Id = 12, Deleted = false, Email = "Luke@example.com", FirstName = "Luke", Surname = "Lukeson" },
                        new { Id = 13, Deleted = false, Email = "Camille@example.com", FirstName = "Camille", Surname = "Camilleson" },
                        new { Id = 14, Deleted = false, Email = "Laura@example.com", FirstName = "Laura", Surname = "Laurason" },
                        new { Id = 15, Deleted = false, Email = "Aidan@example.com", FirstName = "Aidan", Surname = "Aidanson" },
                        new { Id = 16, Deleted = false, Email = "Layla@example.com", FirstName = "Layla", Surname = "Laylason" },
                        new { Id = 17, Deleted = false, Email = "Phil@example.com", FirstName = "Phil", Surname = "Philson" },
                        new { Id = 18, Deleted = false, Email = "Kat@example.com", FirstName = "Kat", Surname = "Katson" },
                        new { Id = 19, Deleted = false, Email = "Peter@example.com", FirstName = "Peter", Surname = "Peterson" },
                        new { Id = 20, Deleted = false, Email = "Val@example.com", FirstName = "Val", Surname = "Valson" }
                    );
                });

            modelBuilder.Entity("ThAmCo.Events.Data.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date");

                    b.Property<TimeSpan?>("Duration");

                    b.Property<bool>("IsActive");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.Property<string>("TypeId")
                        .IsRequired()
                        .IsFixedLength(true)
                        .HasMaxLength(3);

                    b.Property<int>("VenueCapacity");

                    b.Property<double>("VenueCost");

                    b.Property<string>("VenueDescription");

                    b.Property<string>("VenueName");

                    b.Property<string>("VenueRef");

                    b.HasKey("Id");

                    b.ToTable("Events");

                    b.HasData(
                        new { Id = 1, Date = new DateTime(2016, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), Duration = new TimeSpan(0, 6, 0, 0, 0), IsActive = true, Title = "Bob's Big 50", TypeId = "PTY", VenueCapacity = 150, VenueCost = 100.0, VenueDescription = "Once the residence of Lord and Lady Crackling, this lavish dwelling remains a prime example of 18th century fine living.", VenueName = "Crackling Hall", VenueRef = "" },
                        new { Id = 2, Date = new DateTime(2018, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), Duration = new TimeSpan(0, 12, 0, 0, 0), IsActive = true, Title = "Best Wedding Yet", TypeId = "WED", VenueCapacity = 150, VenueCost = 100.0, VenueDescription = "Once the residence of Lord and Lady Crackling, this lavish dwelling remains a prime example of 18th century fine living.", VenueName = "Crackling Hall", VenueRef = "" },
                        new { Id = 3, Date = new DateTime(2018, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), Duration = new TimeSpan(0, 12, 0, 0, 0), IsActive = true, Title = "Billie's Birthday Bonanza", TypeId = "PTY", VenueCapacity = 150, VenueCost = 100.0, VenueDescription = "Refurbished manor house with fully equipped facilities ready to help you have a good time in business or pleasure.", VenueName = "Tinder Manor", VenueRef = "" },
                        new { Id = 4, Date = new DateTime(2019, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), Duration = new TimeSpan(0, 12, 0, 0, 0), IsActive = true, Title = "Stevie's Stag", TypeId = "PYT", VenueCapacity = 150, VenueCost = 100.0, VenueDescription = "Refurbished manor house with fully equipped facilities ready to help you have a good time in business or pleasure.", VenueName = "Tinder Manor", VenueRef = "" },
                        new { Id = 5, Date = new DateTime(2019, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), Duration = new TimeSpan(0, 12, 0, 0, 0), IsActive = true, Title = "Cheryl and Fleur get hitched", TypeId = "WED", VenueCapacity = 150, VenueCost = 100.0, VenueDescription = "Refurbished manor house with fully equipped facilities ready to help you have a good time in business or pleasure.", VenueName = "Tinder Manor", VenueRef = "" },
                        new { Id = 6, Date = new DateTime(2019, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), Duration = new TimeSpan(0, 12, 0, 0, 0), IsActive = true, Title = "George's Divorce Party", TypeId = "PTY", VenueCapacity = 150, VenueCost = 100.0, VenueDescription = "Refurbished manor house with fully equipped facilities ready to help you have a good time in business or pleasure.", VenueName = "Tinder Manor", VenueRef = "" }
                    );
                });

            modelBuilder.Entity("ThAmCo.Events.Data.GuestBooking", b =>
                {
                    b.Property<int>("CustomerId");

                    b.Property<int>("EventId");

                    b.Property<bool>("Attended");

                    b.HasKey("CustomerId", "EventId");

                    b.HasIndex("EventId");

                    b.ToTable("Guests");

                    b.HasData(
                        new { CustomerId = 1, EventId = 1, Attended = true },
                        new { CustomerId = 2, EventId = 1, Attended = false },
                        new { CustomerId = 3, EventId = 1, Attended = false },
                        new { CustomerId = 4, EventId = 1, Attended = false },
                        new { CustomerId = 5, EventId = 1, Attended = false },
                        new { CustomerId = 6, EventId = 1, Attended = false },
                        new { CustomerId = 7, EventId = 1, Attended = false },
                        new { CustomerId = 8, EventId = 1, Attended = false },
                        new { CustomerId = 9, EventId = 1, Attended = false },
                        new { CustomerId = 10, EventId = 1, Attended = false },
                        new { CustomerId = 11, EventId = 1, Attended = false },
                        new { CustomerId = 12, EventId = 1, Attended = false },
                        new { CustomerId = 13, EventId = 1, Attended = false },
                        new { CustomerId = 14, EventId = 1, Attended = false },
                        new { CustomerId = 15, EventId = 1, Attended = false },
                        new { CustomerId = 16, EventId = 1, Attended = false },
                        new { CustomerId = 17, EventId = 1, Attended = false },
                        new { CustomerId = 18, EventId = 1, Attended = false },
                        new { CustomerId = 19, EventId = 1, Attended = false },
                        new { CustomerId = 20, EventId = 4, Attended = false },
                        new { CustomerId = 1, EventId = 2, Attended = false },
                        new { CustomerId = 2, EventId = 2, Attended = false },
                        new { CustomerId = 3, EventId = 2, Attended = false },
                        new { CustomerId = 4, EventId = 2, Attended = false },
                        new { CustomerId = 5, EventId = 2, Attended = false },
                        new { CustomerId = 6, EventId = 2, Attended = false },
                        new { CustomerId = 7, EventId = 2, Attended = false },
                        new { CustomerId = 8, EventId = 2, Attended = false },
                        new { CustomerId = 9, EventId = 2, Attended = false },
                        new { CustomerId = 10, EventId = 3, Attended = false },
                        new { CustomerId = 11, EventId = 3, Attended = false },
                        new { CustomerId = 12, EventId = 3, Attended = false },
                        new { CustomerId = 13, EventId = 3, Attended = false },
                        new { CustomerId = 14, EventId = 3, Attended = false }
                    );
                });

            modelBuilder.Entity("ThAmCo.Events.Data.Staff", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<bool>("FirstAider");

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<bool>("IsActive");

                    b.Property<string>("Surname")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Staff");

                    b.HasData(
                        new { Id = 1, Email = "fred@example.com", FirstAider = true, FirstName = "Fred", IsActive = true, Surname = "Frederickson" },
                        new { Id = 2, Email = "jenny@example.com", FirstAider = false, FirstName = "Jenny", IsActive = true, Surname = "Jenson" },
                        new { Id = 3, Email = "simon@example.com", FirstAider = false, FirstName = "Simon", IsActive = true, Surname = "Simonson" },
                        new { Id = 4, Email = "linda@example.com", FirstAider = false, FirstName = "Linda", IsActive = true, Surname = "Lindason" },
                        new { Id = 5, Email = "tom@example.com", FirstAider = false, FirstName = "Tom", IsActive = true, Surname = "Thompson" },
                        new { Id = 6, Email = "rachel@example.com", FirstAider = false, FirstName = "Rachel", IsActive = true, Surname = "Rachelson" },
                        new { Id = 7, Email = "michael@example.com", FirstAider = false, FirstName = "Mike", IsActive = true, Surname = "Michaelson" }
                    );
                });

            modelBuilder.Entity("ThAmCo.Events.Data.Staffing", b =>
                {
                    b.Property<int>("StaffId");

                    b.Property<int>("EventId");

                    b.HasKey("StaffId", "EventId");

                    b.HasIndex("EventId");

                    b.ToTable("Staffing");

                    b.HasData(
                        new { StaffId = 2, EventId = 1 },
                        new { StaffId = 2, EventId = 3 },
                        new { StaffId = 3, EventId = 3 },
                        new { StaffId = 4, EventId = 4 },
                        new { StaffId = 5, EventId = 4 }
                    );
                });

            modelBuilder.Entity("ThAmCo.Events.Data.GuestBooking", b =>
                {
                    b.HasOne("ThAmCo.Events.Data.Customer", "Customer")
                        .WithMany("Bookings")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ThAmCo.Events.Data.Event", "Event")
                        .WithMany("Bookings")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ThAmCo.Events.Data.Staffing", b =>
                {
                    b.HasOne("ThAmCo.Events.Data.Event", "Event")
                        .WithMany("Staffing")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ThAmCo.Events.Data.Staff", "Staff")
                        .WithMany("Staffing")
                        .HasForeignKey("StaffId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
