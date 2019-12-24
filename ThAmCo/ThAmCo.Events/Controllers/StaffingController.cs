using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThAmCo.Events.Data;
using ThAmCo.Events.Models.Events;
using ThAmCo.Events.Models.GuestBookings;
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
            int numGuests = (await _context.Guests.Include(g => g.Event).Where(g => g.EventId == id).ToListAsync()).Count();
            var staffing = await _context.Staffing.Include(g => g.Staff).Where(g => g.EventId == id).OrderBy(e => e.StaffId).ToListAsync();
            var staff = await _context.Staff.Where(s => s.IsActive == true).Where(e => staffing.Any(b => b.StaffId.Equals(e.Id))).OrderBy(e => e.Id).ToListAsync();
            List<StaffAttendanceVM> staffVM = new List<StaffAttendanceVM>();
            bool firstAiderPresent = false;
            for (int i = 0; i < staffing.Count; i++)
            {
                staffVM.Add(new StaffAttendanceVM(staff[i],true));
                if (firstAiderPresent == false && staffVM[i].FirstAider == true)
                    firstAiderPresent = true;
            }
            BookNewStaffVM eventStaff = new BookNewStaffVM(staffVM, firstAiderPresent, id, @event.Title, numGuests);
            return View(eventStaff);
        }

        public async Task<IActionResult> Create(int eventId)
        {
            int numGuests = (await _context.Guests.Include(g => g.Event).Where(g => g.EventId == eventId).ToListAsync()).Count();
            var unavailableStaff = await _context.Staffing.Include(g => g.Staff).Where(g => g.EventId == eventId).ToListAsync();
            var unStaff = await _context.Staff.Where(e => unavailableStaff.Any(a => a.StaffId.Equals(e.Id))).OrderBy(e => e.Id).ToListAsync();
            var staff = await _context.Staff.Except(unStaff).ToListAsync();
            bool firstAiderPresent = false;
            foreach (Staff s in unStaff)
            {
                if (firstAiderPresent == false && s.FirstAider == true)
                {
                    firstAiderPresent = true;
                }
             }
            List <StaffAttendanceVM> staffVM = new List<StaffAttendanceVM>();
            EventVM @event = new EventVM(await _context.Events.Where(e => e.IsActive == true).FirstOrDefaultAsync(e => e.Id == eventId));
            foreach (Staff s in staff)
            {
                staffVM.Add(new StaffAttendanceVM(s));
            }
            var eventVM = new EventVM(await _context.Events.FindAsync(eventId));
            BookNewStaffVM creator = new BookNewStaffVM(staffVM, firstAiderPresent, eventVM.Id, eventVM.Title,numGuests);
            return View(creator);
        }

        // GET: GuestBookings/Create
        public async Task<IActionResult> AllocateNewEvent(int staffId)
        {
            var unavailableEvents = await _context.Staffing.Include(g => g.Event).Where(g => g.StaffId == staffId).ToListAsync();
            var unevents = await _context.Events.Where(e => unavailableEvents.Any(a => a.EventId.Equals(e.Id))).OrderBy(e => e.Id).ToListAsync();
            var events = await _context.Events.Except(unevents).ToListAsync();
            List<EventVM> eventsVM = new List<EventVM>();
            foreach(Event e in events)
            {
                eventsVM.Add(new EventVM(e));
            }
            var staff = new StaffVM(await _context.Staff.FindAsync(staffId));
            AllocateNewEventVM creator = new AllocateNewEventVM(staff, eventsVM);
            return View(creator);
        }

        // POST: Staffing/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StaffId,EventId,Attended")] StaffBookingVM staffAttendance)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Staffing staff = await _context.Staffing.FindAsync(staffAttendance.StaffId, staffAttendance.EventId);
                    if (staff != null && staffAttendance.Attended == false)
                    {
                        _context.Remove(staff);
                        await _context.SaveChangesAsync();
                        return Ok();
                    }
                    if (staff == null && staffAttendance.Attended == true)
                    {
                        _context.Add(new Staffing(staffAttendance.EventId, staffAttendance.StaffId));
                        await _context.SaveChangesAsync();
                        return Ok();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {

                }
            }
            return NotFound();
        }

        //// POST: Staffing/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> AllocateNewEvent([Bind("StaffId,EventId")] Staffing staffing)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var existingStaff = _context.Staffing.Where(g => g.StaffId == staffing.StaffId && g.EventId == staffing.EventId).ToList();
        //        if (existingStaff == null || existingStaff.Count == 0)
        //        {
        //            _context.Add(staffing);
        //            await _context.SaveChangesAsync();
        //            return RedirectToAction("StaffEvents", new { id = staffing.StaffId });
        //        }
        //        ViewData["CreateMessage"] = "Staff allocation already exists.";
        //    }

        //    return RedirectToAction("StaffEvents", new { id = staffing.StaffId });
        //}

        // GET: Staffing/Delete/5
        public async Task<IActionResult> Delete(int? eventId, int? staffId)
        {
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
