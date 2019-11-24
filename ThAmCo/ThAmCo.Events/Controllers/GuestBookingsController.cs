using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ThAmCo.Events.Data;
using ThAmCo.Events.Models.CustomerEvents;
using ThAmCo.Events.Models.Customers;
using ThAmCo.Events.Models.Events;
using ThAmCo.Events.Models.GuestBookings;

namespace ThAmCo.Events.Controllers
{
    public class GuestBookingsController : Controller
    {
        private readonly EventsDbContext _context;

        public GuestBookingsController(EventsDbContext context)
        {
            _context = context;
        }

        // GET: GuestBookings
        public async Task<IActionResult> Index()
        {
            var eventsDbContext = _context.Guests.Include(g => g.Customer).Include(g => g.Event);
            return View(await eventsDbContext.ToListAsync());
        }

        public async Task<IActionResult> GuestsAtEvent(int id)
        {
            var events = await _context.Events.FirstOrDefaultAsync(m => m.Id == id);
            var bookings = await _context.Guests.Include(g => g.Customer).Where(g => g.EventId == id).OrderBy(e => e.CustomerId).ToListAsync();
            var customer = await _context.Customers.Where(e => bookings.Any(b => b.CustomerId.Equals(e.Id))).OrderBy(e => e.Id).ToListAsync();
            List<CustomerBookingVM> customerBookings = new List<CustomerBookingVM>();
            for (int i = 0; i < bookings.Count; i++)
            {
                customerBookings.Add(new CustomerBookingVM(customer[i], bookings[i]));
            }
            EventCustomersVM eventCustomers = new EventCustomersVM(events, customerBookings);
            return View(eventCustomers);

            //var eventsDbContext = _context.Guests.Include(g => g.Customer).Include(g => g.Event).Where(g =>g.EventId == id);
            //return View("Index",await eventsDbContext.ToListAsync());
        }

        public async Task<IActionResult> CustomerBookings(int id)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(m => m.Id == id);
            var bookings = await _context.Guests.Include(g => g.Event).Where(g => g.CustomerId == id).OrderBy(e => e.EventId).ToListAsync();
            var events = await _context.Events.Where(e => bookings.Any(b => b.EventId.Equals(e.Id))).OrderBy(e => e.Id).ToListAsync();
            List<EventBookingVM> eventBookings = new List<EventBookingVM>();
            for (int i = 0; i < bookings.Count; i++)
            {
                eventBookings.Add(new EventBookingVM(events[i],bookings[i]));
            }
            CustomerEventsVM customerEvents = new CustomerEventsVM(customer, eventBookings);
            return View(customerEvents);
        }

        // POST: GuestBookings/CustomerBookings/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CustomerBookings(int EventId, int CustomerId, [Bind("CustomerId,EventId,Attended")] GuestBooking guestBooking)
        {
            if (CustomerId != guestBooking.CustomerId || EventId != guestBooking.EventId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(guestBooking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GuestBookingExists(guestBooking.CustomerId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Email", guestBooking.CustomerId);
            ViewData["EventId"] = new SelectList(_context.Events, "Id", "Title", guestBooking.EventId);
            return View("CustomerBookings");
        }

        // POST: GuestBookings/Save/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(int CustomerId, int EventId, [Bind("CustomerId,EventId,Attended")] GuestBookingAttendanceVM guestBooking)
        {
            if (CustomerId != guestBooking.CustomerId || EventId != guestBooking.EventId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var dbGuestBooking = await _context.Guests.FindAsync(guestBooking.CustomerId, guestBooking.EventId);
                    dbGuestBooking.Attended = guestBooking.Attended;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GuestBookingExists(guestBooking.CustomerId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                await CustomerBookings(CustomerId);
            }
            return View("CustomerBookings");
        }

        // POST: GuestBookings/Save/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveEventGuests(int CustomerId, int EventId,[Bind("CustomerId,EventId,Attended")] GuestBookingAttendanceVM guestBooking)
        {
            if (CustomerId != guestBooking.CustomerId || EventId != guestBooking.EventId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var dbGuestBooking = await _context.Guests.FindAsync(guestBooking.CustomerId, guestBooking.EventId);
                    dbGuestBooking.Attended = guestBooking.Attended;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GuestBookingExists(guestBooking.CustomerId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                await GuestsAtEvent(EventId);
            }
            return View("GuestsAtEvent");
        }

        // GET: GuestBookings/Delete/5
        public async Task<IActionResult> Delete(int? eventId, int? customerId)
        {
            ViewData["ErrorMessage"] = "";
            if (eventId == null || customerId == null)
            {
                return NotFound();
            }
            var guestBooking = await _context.Guests.FindAsync(customerId, eventId);
            if (guestBooking == null)
            {
                return NotFound();
            }
            var customer = new CustomerVM(await _context.Customers.FindAsync(customerId));
            var @event = new EventVM(await _context.Events.FindAsync(eventId));
            var guestBookingVM = new GuestBookingVM(guestBooking,customer,@event);
            return View(guestBookingVM);
        }

        // POST: GuestBookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int eventId, int customerId)
        {
            var guestBooking = await _context.Guests.FindAsync(customerId, eventId);
            _context.Guests.Remove(guestBooking);
            await _context.SaveChangesAsync();
            //return CustomerBookings(customerId);
            return RedirectToAction("CustomerBookings", new { id = customerId });
        }

        // GET: GuestBookings/Create
        public async Task<IActionResult> Create(int customerId)
        {
            var unavailableEvents = await _context.Guests.Include(g => g.Event).Where(g => g.CustomerId == customerId).ToListAsync();
            var unevents = await _context.Events.Where(e => unavailableEvents.Any(a => a.EventId.Equals(e.Id))).OrderBy(e => e.Id).ToListAsync();
            var events = await _context.Events.Except(unevents).ToListAsync();
            var eventList = new SelectList(events, "Id", "Title");
            var customer = new CustomerVM(await _context.Customers.FindAsync(customerId));
            GuestBookingCreateVM creator = new GuestBookingCreateVM(customer, eventList);
            return View(creator);
        }

        // POST: GuestBookings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int CustomerId, int EventId, [Bind("CustomerId,EventId,Attended")] GuestBooking guestBooking)
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Email", guestBooking.CustomerId);
            ViewData["EventId"] = new SelectList(_context.Events, "Id", "Title", guestBooking.EventId);
            ViewData["GuestBookingMessage"] = "";

            if (ModelState.IsValid)
            {
                var existingGuest = _context.Guests.Where(g => g.CustomerId == guestBooking.CustomerId && g.EventId == guestBooking.EventId).ToList();
                if (existingGuest == null || existingGuest.Count == 0)
                {
                    _context.Add(guestBooking);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("CustomerBookings", new { id = guestBooking.CustomerId });
                }
                ViewData["GuestBookingMessage"] = "Booking already exists.";
            }

            return RedirectToAction("CustomerBookings", new { id = guestBooking.CustomerId });
        }

        private bool GuestBookingExists(int id)
        {
            return _context.Guests.Any(e => e.CustomerId == id);
        }
        //// GET: GuestBookings/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }
        //    var guestBooking = await _context.Guests
        //        .Include(g => g.Customer)
        //        .Include(g => g.Event)
        //        .FirstOrDefaultAsync(m => m.CustomerId == id);
        //    if (guestBooking == null)
        //    {
        //        return NotFound();
        //    }
        //    var guestBookingVM = new ViewModels.GuestBookings.GuestBookingVM(guestBooking);
        //    return View(guestBooking);
        //}


        //// GET: GuestBookings/Edit/5
        //public async Task<IActionResult> Edit(int? eventId, int? customerId)
        //{
        //    if (eventId == null || customerId == null)
        //    {
        //        return NotFound();
        //    }

        //    var guestBooking = await _context.Guests.FindAsync(customerId,eventId);
        //    if (guestBooking == null)
        //    {
        //        return NotFound();
        //    }
        //    var guestBookingVM = new ViewModels.GuestBookings.GuestBookingVM(guestBooking);
        //    //ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Email", customerId);
        //    //ViewData["EventId"] = new SelectList(_context.Events, "Id", "Title", eventId);
        //    return View(guestBookingVM);
        //}

        //// POST: GuestBookings/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int EventId, int CustomerId, [Bind("CustomerId,EventId,Attended")] GuestBooking guestBooking)
        //{
        //    if (CustomerId != guestBooking.CustomerId || EventId != guestBooking.EventId)
        //    {
        //        return NotFound();
        //    }
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(guestBooking);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!GuestBookingExists(guestBooking.CustomerId))
        //            {
        //                return NotFound();
        //            }
        //            else
        //           {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Email", guestBooking.CustomerId);
        //    ViewData["EventId"] = new SelectList(_context.Events, "Id", "Title", guestBooking.EventId);
        //    return View("CustomerBookings");
        //}


    }
}
