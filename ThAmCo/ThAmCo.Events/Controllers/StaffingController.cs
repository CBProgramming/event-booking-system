using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    //Staffing controller to manage staffing CRUD
    //View models used throughout to separate processes from backend database
    public class StaffingController : Controller

    {
        private readonly EventsDbContext _context;

        public StaffingController(EventsDbContext context)
        {
            _context = context;
        }

        //// GET: GuestBookings
        //public async Task<IActionResult> Index()
        //{
        //    var eventsDbContext = _context.Staffing.Include(s => s.Event).Include(s => s.Staff);
        //    return View(await eventsDbContext.ToListAsync());
        //}

        //Returns list of staff at event, based on staff id
        //Provides warning messages regarding needing more staff or first aider
        //Staff can be removed or re-added in bulk in this view via ajax
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

        //Display list of staff currently not allocated to event, based on event id provided
        //Staff can be added or removed in bulk in this view via ajax
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

        //Display list of events that staff member is currently not allocated to, based on staff id provided
        //Staff member can be added or removed to/from events in bulk in this view via ajax
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

        //Creates or removes new staffing record based on user input
        //Called via ajax to prevent page reloading and to allow bulk staffing allocation
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

        // Returns confirmation page to delete staff from event
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

        //Removes staffing allocation of staff to event based on staff and event ids
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int eventId, int staffId)
        {
            var staffBooking = await _context.Staffing.FindAsync(staffId, eventId);
            _context.Staffing.Remove(staffBooking);
            await _context.SaveChangesAsync();
            return RedirectToAction("StaffAtEvent", new { id = eventId });
        }

        //Returns a view listing all events a staff member is currently allocated to, based on staff id
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
