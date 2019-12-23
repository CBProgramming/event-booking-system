using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThAmCo.Catering.Data
{
    public class MenusDbContext : DbContext
    {
        public DbSet<Menu> Menus { get; set; }
        public DbSet<FoodBooking> FoodBookings { get; set; }

        private readonly IHostingEnvironment _hostEnv;

        public MenusDbContext(DbContextOptions<MenusDbContext> options,
                               IHostingEnvironment env) : base(options)
        {
            _hostEnv = env;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.HasDefaultSchema("thamco.menus");

            builder.Entity<Menu>()
                   .HasMany(c => c.Bookings)
                   .WithOne(b => b.Menu);

            builder.Entity<FoodBooking>()
                .HasKey(d => d.EventId);

            builder.Entity<FoodBooking>()
                .Property(d => d.EventId)
                .ValueGeneratedNever();

            if (_hostEnv != null && _hostEnv.IsDevelopment())
            {
                builder.Entity<Menu>()
                       .HasData(
                            new Menu { MenuId = 1, Name = "The Banquet Menu", CostPerHead = 10.50, Starter = "Butternut Squash Soup", Main = "Ham hock and seasonal vegetables", Dessert = "Forest fruit gateaux" },
                            new Menu { MenuId = 2, Name = "The Budget Bonanza", CostPerHead = 15.25, Starter = "Salt and Pepper Chips", Main = "The Megaburger", Dessert = "New York Cheesecake" },
                            new Menu { MenuId = 3, Name = "The Overpiced Special", CostPerHead = 20.00, Starter = "Dry toast", Main = "Mashed potato and beans", Dessert = "Jelly" }
                        );

                builder.Entity<FoodBooking>()
                       .HasData(
                            new FoodBooking { EventId = 1, MenuNumber = 1 },
                            new FoodBooking { EventId = 2, MenuNumber = 2 },
                            new FoodBooking { EventId = 3, MenuNumber = 3 },
                            new FoodBooking { EventId = 4, MenuNumber = 1 }
                       );
            }
        }
    }
}