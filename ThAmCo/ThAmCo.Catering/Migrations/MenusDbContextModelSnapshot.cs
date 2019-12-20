﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ThAmCo.Catering.Data;

namespace ThAmCo.Catering.Migrations
{
    [DbContext(typeof(MenusDbContext))]
    partial class MenusDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("thamco.menus")
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ThAmCo.Catering.Data.FoodBooking", b =>
                {
                    b.Property<int>("MenuId");

                    b.Property<int>("EventId");

                    b.HasKey("MenuId", "EventId");

                    b.ToTable("FoodBookings");

                    b.HasData(
                        new { MenuId = 1, EventId = 1 },
                        new { MenuId = 2, EventId = 2 },
                        new { MenuId = 3, EventId = 3 },
                        new { MenuId = 1, EventId = 4 }
                    );
                });

            modelBuilder.Entity("ThAmCo.Catering.Data.Menu", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("CostPerHead");

                    b.Property<string>("Dessert");

                    b.Property<string>("Main");

                    b.Property<string>("Name");

                    b.Property<string>("Starter");

                    b.HasKey("Id");

                    b.ToTable("Menus");

                    b.HasData(
                        new { Id = 1, CostPerHead = 10.5, Dessert = "Forest fruit gateaux", Main = "Ham hock and seasonal vegetables", Name = "The Banquet Menu", Starter = "Butternut Squash Soup" },
                        new { Id = 2, CostPerHead = 15.25, Dessert = "New York Cheesecake", Main = "The Megaburger", Name = "The Budget Bonanza", Starter = "Salt and Pepper Chips" },
                        new { Id = 3, CostPerHead = 20.0, Dessert = "Jelly", Main = "Mashed potato and beans", Name = "The Overpiced Special", Starter = "Dry toast" }
                    );
                });

            modelBuilder.Entity("ThAmCo.Catering.Data.FoodBooking", b =>
                {
                    b.HasOne("ThAmCo.Catering.Data.Menu", "Menu")
                        .WithMany("Bookings")
                        .HasForeignKey("MenuId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
