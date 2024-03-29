﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ThAmCo.Catering.Data;

namespace ThAmCo.Catering.Migrations
{
    [DbContext(typeof(MenusDbContext))]
    [Migration("20191223111842_FoodMigrationFix3")]
    partial class FoodMigrationFix3
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("thamco.menus")
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ThAmCo.Catering.Data.FoodBooking", b =>
                {
                    b.Property<int>("EventId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("MenuId");

                    b.Property<int>("MenuNumber");

                    b.HasKey("EventId");

                    b.HasIndex("MenuId");

                    b.ToTable("FoodBookings");

                    b.HasData(
                        new { EventId = 1, MenuNumber = 1 },
                        new { EventId = 2, MenuNumber = 2 },
                        new { EventId = 3, MenuNumber = 3 },
                        new { EventId = 4, MenuNumber = 1 },
                        new { EventId = 5, MenuNumber = 2 },
                        new { EventId = 6, MenuNumber = 3 }
                    );
                });

            modelBuilder.Entity("ThAmCo.Catering.Data.Menu", b =>
                {
                    b.Property<int>("MenuId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("CostPerHead");

                    b.Property<string>("Dessert");

                    b.Property<string>("Main");

                    b.Property<string>("Name");

                    b.Property<string>("Starter");

                    b.HasKey("MenuId");

                    b.ToTable("Menus");

                    b.HasData(
                        new { MenuId = 1, CostPerHead = 10.5, Dessert = "Forest fruit gateaux", Main = "Ham hock and seasonal vegetables", Name = "The Banquet Menu", Starter = "Butternut Squash Soup" },
                        new { MenuId = 2, CostPerHead = 15.25, Dessert = "New York Cheesecake", Main = "The Megaburger", Name = "The Budget Bonanza", Starter = "Salt and Pepper Chips" },
                        new { MenuId = 3, CostPerHead = 20.0, Dessert = "Jelly", Main = "Mashed potato and beans", Name = "The Overpiced Special", Starter = "Dry toast" }
                    );
                });

            modelBuilder.Entity("ThAmCo.Catering.Data.FoodBooking", b =>
                {
                    b.HasOne("ThAmCo.Catering.Data.Menu", "Menu")
                        .WithMany("Bookings")
                        .HasForeignKey("MenuId");
                });
#pragma warning restore 612, 618
        }
    }
}
