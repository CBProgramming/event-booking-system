using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThAmCo.Events.Data;
using ThAmCo.Events.Models.Staff;
using ThAmCo.Events.Models.Staffing;

namespace ThAmCo.Events.Controllers
{
    public class StaffingController : Controller

    {
        private readonly EventsDbContext _context;

        public StaffingController(EventsDbContext context)
        {
            _context = context;
        }

        // GET: GuestBookings
        public async Task<IActionResult> Index()
        {
            var eventsDbContext = _context.Staffing.Include(s => s.Event).Include(s => s.Staff);
            return View(await eventsDbContext.ToListAsync());
        }
    }
}
