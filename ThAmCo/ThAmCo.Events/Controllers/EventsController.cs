using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ThAmCo.Events.Data;
using ThAmCo.Events.Models.Availability;
using ThAmCo.Events.Models.Events;
using ThAmCo.Events.Models.Venues;
using ThAmCo.Events.Services;

namespace ThAmCo.Events.Controllers
{
    public class EventsController : Controller
    {
        private readonly EventsDbContext _context;
        private readonly IConfiguration _configuration;

        public EventsController(EventsDbContext context,
                                IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: Events
        public async Task<IActionResult> Index()
        {
            var eventsVM = new Models.Events.EventsVM(await _context.Events.ToListAsync());
            List<EventVM> events = new List<EventVM>();
            foreach (Event e in eventsVM.Events)
            {
                events.Add(new EventVM(e));
            }
            return View(events);
        }

        // GET: Events/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var @event = await _context.Events
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@event == null)
            {
                return NotFound();
            }
            var eventVM = new EventVM(@event);
            return View(eventVM);
        }


        // GET: Events/Create
        public IActionResult Create(EventVM @event)
        {
            return View(@event);
        }

        // POST: Events/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Date,Duration,TypeId")] Event @event)
        {
            if (ModelState.IsValid)
            {
                _context.Add(@event);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(@event);
        }

        // GET: Events/Edit/5
        public async Task<IActionResult> Edit(int? id, string message)
        {
            if (id == null)
            {
                return NotFound();
            }
            var @event = await _context.Events.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }
            var eventVM = new EventVM(@event, true);
            eventVM.Message = message;
            //EventToEditVM eventEditor = new EventToEditVM(eventVM);
            return View(eventVM);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Date,Duration,TypeId,VenueRef,Existing,VenueName,VenueDescription,VenueCapacity,VenueCost,OldRef")] EventVM @event)
        {
            if (id != @event.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var dbEvent = await _context.Events.FindAsync(@event.Id);
                    dbEvent.Title = @event.Title;
                    dbEvent.Duration = @event.Duration;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(@event.Id))
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
            return View(@event);
        }


        public IActionResult SelectVenue([Bind("Id,Title,Date,Duration,TypeId,VenueRef,Existing,VenueName,VenueDescription,VenueCapacity,VenueCost,OldRef")] EventVM @event)
        {
            if (@event.Existing)
               @event.OldRef = @event.getBookingRef;
            List<AvailabilitiesVM> availabilities = GetAvailability(@event.TypeId, @event.Date, @event.Date).Result.ToList();
            if (availabilities.Count == 0)
            {
                @event.Message = "No venues available on this date";
                if (@event.Existing == false)
                    return View("Create", @event);
                else
                    return RedirectToAction("Edit", new { Id = @event.Id, Message = @event.Message });
            }
            else
            {
                EventVenueAvailabilityVM venueSelector = new EventVenueAvailabilityVM(@event, availabilities);
                return View(venueSelector);
            }
        }

        public IActionResult ConfirmReservation(string eventName, TimeSpan duration, string type, string code, DateTime date, string venueName,
                                                string venueDescription, int venueCapacity, double venueCost, bool existing, string oldRef, int eventId)
        {
            EventVM eventVM = new EventVM(eventId, eventName, date, duration, type,venueName,venueDescription,venueCapacity,venueCost,existing, code, oldRef);
            EventVenueVM selectedEventVenue = new EventVenueVM(eventVM, code, date, venueName);
            return View(selectedEventVenue);
        }

        [HttpPost]
        public async Task<IActionResult> BookEvent([Bind("Id,VenueRef,Date,VenueName,VenueDescription,VenueCapacity,VenueCost,Title,Duration,TypeId,Existing,OldRef")] EventVM booking)
        {
            var client = setupVenueClient();
            string uri = "/api/Reservations";
            string uriOldRef = uri + "/" + booking.OldRef;
            ReservationPostDto res = new ReservationPostDto(booking.Date, booking.VenueRef);
            try
            {
                //var getResponse = await client.GetAsync(uriOldRef);
                if ((await client.GetAsync(uriOldRef)).IsSuccessStatusCode)
                {
                    var deleteResponse = await client.DeleteAsync(uriOldRef);
                    deleteResponse.EnsureSuccessStatusCode();
                }
                var postResponse = await client.PostAsJsonAsync(uri, res);
                postResponse.EnsureSuccessStatusCode();
                if(booking.Existing)  // need the ID!
                {
                    var @event = await _context.Events.FindAsync(booking.Id);
                    @event.Title = booking.Title;
                    @event.Duration = booking.Duration;
                    @event.VenueRef = booking.VenueRef;
                    @event.VenueName = booking.VenueName;
                    @event.VenueDescription = booking.VenueDescription;
                    @event.VenueCapacity = booking.VenueCapacity;
                    @event.VenueCost = booking.VenueCost;
                    await _context.SaveChangesAsync();
                }
                else
                {
                    Event @event = new Event();
                    @event.Date = booking.Date;
                    @event.Title = booking.Title;
                    @event.Duration = booking.Duration;
                    @event.TypeId = booking.TypeId;
                    @event.VenueRef = booking.VenueRef;
                    @event.VenueName = booking.VenueName;
                    @event.VenueDescription = booking.VenueDescription;
                    @event.VenueCapacity = booking.VenueCapacity;
                    @event.VenueCost = booking.VenueCost;
                    _context.Add(@event);
                    await _context.SaveChangesAsync();
                }

            }
            catch (Exception)
            {

            }
            return RedirectToAction(nameof(Index));
        }








        // GET: Events/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var @event = await _context.Events
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@event == null)
            {
                return NotFound();
            }
            var eventVM = new Models.Events.EventVM(@event);
            return View(eventVM);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @event = await _context.Events.FindAsync(id);
            _context.Events.Remove(@event);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventExists(int id)
        {
            return _context.Events.Any(e => e.Id == id);
        }



        //IEnumerable<AvailabilityGetDto>
        private async Task<IEnumerable<AvailabilitiesVM>> GetAvailability (string eventType, DateTime beginDate, DateTime endDate)
        {
            //DateTime endDateTest = new DateTime(2021, 01, 01);

            var client = new HttpClient();
            client.BaseAddress = new Uri(_configuration["VenuesBaseURI"]);
            client.DefaultRequestHeaders.Accept.ParseAdd("application/json");
            client.Timeout = TimeSpan.FromSeconds(5);
            string startDate = stringDate(beginDate);
            string finishDate = stringDate(endDate);
            IEnumerable<AvailabilitiesVM> availabilities;
            try
            {
                string uri = "/api/Availability";
                string uriEventType = "?eventType=" + eventType;
                string uriBeginDate = "&beginDate=" + startDate;
                string uriEndDate = "&endDate=" + finishDate;
                //string uriEndDate = "&endDate=" + endDateTest;
                var response = await client.GetAsync(uri + uriEventType + uriBeginDate + uriEndDate);
                response.EnsureSuccessStatusCode();
                availabilities =  await response.Content.ReadAsAsync<IEnumerable<AvailabilitiesVM>>();
            }
            catch (HttpRequestException e)
            {
                //_logger.LogError("Bad response from Availabilities");
                availabilities = Array.Empty<AvailabilitiesVM>();
            }
            return availabilities;
        }

        private string geenerateReservationRef(string code, DateTime date)
        {
            string day = date.Day.ToString("00");
            string month = date.Month.ToString("00");
            string year = date.Year.ToString("0000");
            string reference = code + year + month + day;
            return reference;
        }

        private HttpClient setupVenueClient()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(_configuration["VenuesBaseURI"]);
            client.DefaultRequestHeaders.Accept.ParseAdd("application/json");
            client.Timeout = TimeSpan.FromSeconds(5);
            return client;
        }

        public string stringDate(DateTime date)
        {
            string stringDate = date.Month.ToString("00") + "/" + date.Day.ToString("00") + "/" + date.Year.ToString("0000") + " "
                                + date.Hour.ToString("00") + ":" + date.Minute.ToString("00") + ":" + date.Second.ToString("00");
            return stringDate;
        }
    }
}
