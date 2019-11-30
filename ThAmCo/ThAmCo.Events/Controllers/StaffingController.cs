using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThAmCo.Events.Data;
using ThAmCo.Events.Models.Events;
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

        public async Task<IActionResult> StaffAtEvent(int id)
        {
            var @event = await _context.Events.FirstOrDefaultAsync(m => m.Id == id);
            var staffing = await _context.Staffing.Include(g => g.Staff).Where(g => g.EventId == id).OrderBy(e => e.StaffId).ToListAsync();
            var staff = await _context.Staff.Where(s => s.IsActive == true).Where(e => staffing.Any(b => b.StaffId.Equals(e.Id))).OrderBy(e => e.Id).ToListAsync();
            List<StaffVM> staffVM = new List<StaffVM>();
            bool firstAiderPresent = false;
            for (int i = 0; i < staffing.Count; i++)
            {
                staffVM.Add(new StaffVM(staff[i]));
                if (firstAiderPresent == false && staffVM[i].FirstAider == true)
                    firstAiderPresent = true;
            }
            EventStaffVM eventStaff = new EventStaffVM(staffVM, firstAiderPresent, id, @event.Title);
            return View(eventStaff);
        }
    }
}
