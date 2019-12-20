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

            builder.Entity<FoodBooking>()
                   .HasKey(b => new { b.MenuId, b.EventId });

            builder.Entity<Menu>()
                   .HasMany(c => c.Bookings)
                   .WithOne(b => b.Menu)
                   .HasForeignKey(b => b.MenuId);



            if (_hostEnv != null && _hostEnv.IsDevelopment())
            {
                builder.Entity<Menu>()
                       .HasData(
                            new Menu { Id = 1, Name = "The Banquet Menu", CostPerHead = 10.50, Starter = "Butternut Squash Soup", Main = "Ham hock and seasonal vegetables", Dessert = "Forest fruit gateaux" },
                            new Menu { Id = 2, Name = "The Budget Bonanza", CostPerHead = 15.25, Starter = "Salt and Pepper Chips", Main = "The Megaburger", Dessert = "New York Cheesecake" },
                            new Menu { Id = 3, Name = "The Overpiced Special", CostPerHead = 20.00, Starter = "Dry toast", Main = "Mashed potato and beans", Dessert = "Jelly" }
                        );

                builder.Entity<FoodBooking>()
                       .HasData(
                            new FoodBooking { EventId = 1, MenuId = 1 },
                            new FoodBooking { EventId = 2, MenuId = 2 },
                            new FoodBooking { EventId = 3, MenuId = 3 },
                            new FoodBooking { EventId = 4, MenuId = 1 }
                       );
            }
        }
    }
}