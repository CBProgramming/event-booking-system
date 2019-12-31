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

    //GuestBookings controller to manage guestbooking CRUD
    //View models used throughout to separate processes from backend database
    public class GuestBookingsController : Controller
    {
        private readonly EventsDbContext _context;

        public GuestBookingsController(EventsDbContext context)
        {
            _context = context;
        }

        //Returns view of all guests at an event, based on event id provided
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

        //Sets a guest bookings attended field to true or false based on user input
        //Accessed via ajax in multiple sections of solution to prevent reloading of pages, improving user experience
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

        //Returns a view listing all events with which the customer is booked onto
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

        // Reusable method which returns confirmation view to ensure user wishes to cancel the guest booking
        // Origin string used to check which route the user used to cet to this screen, ensuring they are returned 
        //to the correct on on completion of the process
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

        // Deletes a guest booking from database and then uses origin string to return user to correct page
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

        // Displays a list of all events a customer is not booked onto with a tick box to allow multiple bookings from one screen
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

        // Returns a list of customers not currently attending an event, based on event id provided
        // Allows for bulk bookings onto an event using ajax
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

        //Creates a guest booking based on customer and event id
        // Called via ajax to prevent page reloading and to allow bulk bookings via a list of events or customers
        [HttpPost]
        public async Task<IActionResult> Create([Bind("CustomerId,EventId,Attended")] GuestBooking guestBooking)
        {
            if (ModelState.IsValid)
            {
                var existingGuest = _context.Guests.Where(g => g.CustomerId == guestBooking.CustomerId && g.EventId == guestBooking.EventId).ToList();
                if ((existingGuest == null || existingGuest.Count == 0) && guestBooking.Attended == true)
                {
                    var allGuests = _context.Guests.Where(g => g.EventId == guestBooking.EventId).ToList();
                    EventVM eventVM = new EventVM(await _context.Events.FindAsync(guestBooking.EventId));
                    if (allGuests.Count >= eventVM.VenueCapacity)
                    {
                        return BadRequest();
                    }
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

        //Reusable method to check if guest booking exists
        private bool GuestBookingExists(int id)
        {
            return _context.Guests.Any(e => e.CustomerId == id);
        }
    }
}
