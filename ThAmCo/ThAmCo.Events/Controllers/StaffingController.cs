using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            int numGuests = (await _context.Guests.Include(g => g.Event).Where(g => g.EventId == e.Id).ToListAsync()).Count();
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
            EventStaffVM eventStaff = new EventStaffVM(staffVM, firstAiderPresent, id, @event.Title, numGuests);
            return View(eventStaff);
        }

        public async Task<IActionResult> Create(int eventId)
        {
            var unavailableStaff = await _context.Staffing.Include(g => g.Staff).Where(g => g.EventId == eventId).ToListAsync();
            var unStaff = await _context.Staff.Where(e => unavailableStaff.Any(a => a.StaffId.Equals(e.Id))).OrderBy(e => e.Id).ToListAsync();
            var staff = await _context.Staff.Except(unStaff).ToListAsync();
            var staffList = new SelectList(staff, "Id", "FullName");
            var eventVM = new EventVM(await _context.Events.FindAsync(eventId));
            BookNewStaffVM creator = new BookNewStaffVM(eventVM, staffList);
            return View(creator);
        }

        // GET: GuestBookings/Create
        public async Task<IActionResult> AllocateNewEvent(int staffId)
        {
            var unavailableEvents = await _context.Staffing.Include(g => g.Event).Where(g => g.StaffId == staffId).ToListAsync();
            var unevents = await _context.Events.Where(e => unavailableEvents.Any(a => a.EventId.Equals(e.Id))).OrderBy(e => e.Id).ToListAsync();
            var events = await _context.Events.Except(unevents).ToListAsync();
            var eventList = new SelectList(events, "Id", "Title");
            var staff = new StaffVM(await _context.Staff.FindAsync(staffId));
            AllocateNewEventVM creator = new AllocateNewEventVM(staff, eventList);
            return View(creator);
        }

        // POST: Staffing/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StaffId,EventId")] Staffing staffing)
        {
            if (ModelState.IsValid)
            {
                var existingStaff = _context.Staffing.Where(g => g.StaffId == staffing.StaffId && g.EventId == staffing.EventId).ToList();
                if (existingStaff == null || existingStaff.Count == 0)
                {
                    _context.Add(staffing);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("StaffAtEvent", new { id = staffing.EventId });
                }
                ViewData["CreateMessage"] = "Booking already exists.";
            }

            return RedirectToAction("StaffAtEvent", new { id = staffing.EventId });
        }

        // POST: Staffing/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AllocateNewEvent([Bind("StaffId,EventId")] Staffing staffing)
        {
            if (ModelState.IsValid)
            {
                var existingStaff = _context.Staffing.Where(g => g.StaffId == staffing.StaffId && g.EventId == staffing.EventId).ToList();
                if (existingStaff == null || existingStaff.Count == 0)
                {
                    _context.Add(staffing);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("StaffEvents", new { id = staffing.StaffId });
                }
                ViewData["CreateMessage"] = "Staff allocation already exists.";
            }

            return RedirectToAction("StaffEvents", new { id = staffing.StaffId });
        }

        // GET: Staffing/Delete/5
        public async Task<IActionResult> Delete(int? eventId, int? staffId)
        {
            ViewData["ErrorMessage"] = "";
            if (eventId == null || staffId == null)
            {
                return NotFound();
            }
            var staffing = await _context.Staffing.FindAsync(staffId, eventId);
            if (staffing == null)
            {
                return NotFound();
            }
            var staff = new StaffVM(await _context.Staff.FindAsync(staffId));
            var @event = new EventVM(await _context.Events.FindAsync(eventId));

            StaffingVM staffingVM = new StaffingVM(staffing, staff, @event);
            return View(staffingVM);
        }

        // POST: Staffing/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int eventId, int staffId)
        {
            var staffBooking = await _context.Staffing.FindAsync(staffId, eventId);
            _context.Staffing.Remove(staffBooking);
            await _context.SaveChangesAsync();
            return RedirectToAction("StaffAtEvent", new { id = eventId });
        }

        public async Task<IActionResult> StaffEvents(int id)
        {
            StaffVM staffVM = new StaffVM(await _context.Staff.FirstOrDefaultAsync(m => m.Id == id));
            var staffing = await _context.Staffing.Include(g => g.Event).Where(g => g.StaffId == id).OrderBy(e => e.EventId).ToListAsync();
            var events = await _context.Events.Where(e => staffing.Any(b => b.EventId.Equals(e.Id))).OrderBy(e => e.Id).ToListAsync();
            List<EventVM> eventStaffing = new List<EventVM>();
            for (int i = 0; i < staffing.Count; i++)
            {
                eventStaffing.Add(new EventVM(events[i]));
            }
            StaffEventsVM customerEvents = new StaffEventsVM(staffVM, eventStaffing);
            return View(customerEvents);
        }
    }
}
