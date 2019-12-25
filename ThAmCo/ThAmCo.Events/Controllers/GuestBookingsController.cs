using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
            var bookings = await _context.Guests.Include(g => g.Customer).Where(g => g.Customer.Deleted == false).Where(g => g.EventId == id).OrderBy(e => e.CustomerId).ToListAsync();
            var customer = await _context.Customers.Where(c => c.Deleted == false).Where(e => bookings.Any(b => b.CustomerId.Equals(e.Id))).OrderBy(e => e.Id).ToListAsync();
            List<CustomerBookingVM> customerBookings = new List<CustomerBookingVM>();
            for (int i = 0; i < bookings.Count; i++)
            {
                customerBookings.Add(new CustomerBookingVM(customer[i], bookings[i]));
            }
            EventCustomersVM eventCustomers = new EventCustomersVM(events, customerBookings);
            return View(eventCustomers);
        }

        // POST: GuestBookings/Save/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> GuestsAtEvent(int CustomerId, int EventId, [Bind("CustomerId,EventId,Attended")] GuestBookingAttendanceVM guestBooking)
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
                    return Ok();
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
            }
            return Ok();
        }

        public async Task<IActionResult> CustomerBookings(int id)
        {
            var customer = await _context.Customers.Where(c => c.Deleted == false).FirstOrDefaultAsync(m => m.Id == id);
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

        // GET: GuestBookings/Delete/5
        public async Task<IActionResult> Delete(int? eventId, int? customerId, string origin)
        {
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
            if (customer.Deleted == true)
            {
                return NotFound();
            }
            var @event = new EventVM(await _context.Events.FindAsync(eventId));
            var guestBookingVM = new GuestBookingVM(guestBooking,customer,@event);
            guestBookingVM.Origin = origin;
            return View(guestBookingVM);
        }

        // POST: GuestBookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int eventId, int customerId, string origin)
        {
            var guestBooking = await _context.Guests.FindAsync(customerId, eventId);
            _context.Guests.Remove(guestBooking);
            await _context.SaveChangesAsync();
            if (origin == "CustomerBookings")
            {
                return RedirectToAction("CustomerBookings", new { id = customerId });
            }
            else if (origin == "GuestsAtEvent")
            {
                return RedirectToAction("GuestsAtEvent", new { id = eventId });
            }
            else //something went wrong
            {
                return RedirectToAction("Index", "Events");
            }

        }

        // GET: GuestBookings/Create
        public async Task<IActionResult> Create(int customerId)
        {
            var customer = new CustomerVM(await _context.Customers.FindAsync(customerId));
            if (customer.Deleted == true)
            {
                return NotFound();
            }
            var unavailableEvents = await _context.Guests.Include(g => g.Event).Where(g => g.CustomerId == customerId).ToListAsync();
            var unevents = await _context.Events.Where(e => unavailableEvents.Any(a => a.EventId.Equals(e.Id))).OrderBy(e => e.Id).ToListAsync();
            var events = await _context.Events.Except(unevents).ToListAsync();
            List<EventVM> eventsVM = new List<EventVM>();
            foreach (Event e in events)
            {
                eventsVM.Add(new EventVM(e));
            }
            GuestBookingCreateVM creator = new GuestBookingCreateVM(customer, eventsVM);
            return View(creator);
        }

        // GET: GuestBookings/Create
        public async Task<IActionResult> BookNewGuest(int eventId)
        {
            var unavailableCustomers = await _context.Guests.Include(g => g.Customer).Where(g => g.EventId == eventId).ToListAsync();
            var unCustomers = await _context.Customers.Where(c => c.Deleted == false).Where(e => unavailableCustomers.Any(a => a.CustomerId.Equals(e.Id))).OrderBy(e => e.Id).ToListAsync();
            var customers = await _context.Customers.Where(c => c.Deleted == false).Except(unCustomers).ToListAsync();
            List<CustomerVM> customersVM = new List<CustomerVM>();
            foreach (Customer c in customers)
            {
                customersVM.Add(new CustomerVM(c));
            }
            var eventVM = new EventVM(await _context.Events.FindAsync(eventId));
            BookNewGuestVM creator = new BookNewGuestVM(eventVM, customersVM);
            return View(creator);
        }

        // POST: GuestBookings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create(int CustomerId, int EventId, [Bind("CustomerId,EventId,Attended")] GuestBooking guestBooking)
        {
            if (ModelState.IsValid)
            {
                var existingGuest = _context.Guests.Where(g => g.CustomerId == guestBooking.CustomerId && g.EventId == guestBooking.EventId).ToList();
                if ((existingGuest == null || existingGuest.Count == 0) && guestBooking.Attended == true)
                {
                    guestBooking.Attended = false;
                    _context.Add(guestBooking);
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                else if(existingGuest != null && existingGuest.Count != 0 && guestBooking.Attended == false)
                {
                    _context.Remove(existingGuest.FirstOrDefault());
                    await _context.SaveChangesAsync();
                    return Ok();
                }
            }
            return BadRequest();
        }



        // POST: GuestBookings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BookNewGuest(int CustomerId, int EventId, [Bind("CustomerId,EventId,Attended")] GuestBooking guestBooking)
        {

            if (ModelState.IsValid)
            {
                var existingGuest = _context.Guests.Where(g => g.CustomerId == guestBooking.CustomerId && g.EventId == guestBooking.EventId).ToList();
                if (existingGuest == null || existingGuest.Count == 0)
                {
                    _context.Add(guestBooking);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("GuestsAtEvent", new { id = guestBooking.EventId });
                }
            }

            return RedirectToAction("GuestsAtEvent", new { id = guestBooking.EventId });
        }

        private bool GuestBookingExists(int id)
        {
            return _context.Guests.Any(e => e.CustomerId == id);
        }
    }
}
