using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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

        public EventsController(EventsDbContext context)
        {
            _context = context;
        }

        // GET: Events
        public async Task<IActionResult> Index()
        {
            var eventsVM = new Models.Events.EventsVM(await _context.Events.ToListAsync());
            return View(eventsVM);
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
            var eventVM = new Models.Events.EventVM(@event);
            return View(eventVM);
        }

        // GET: Events/Create
        public IActionResult Create()
        {
            return View();
        }


        //[HttpPost]
        public IActionResult SelectVenue([Bind("Id,Title,Date,Duration,TypeId")] EventVM @event)
        {

            List<AvailabilitiesVM> availabilities = GetAvailability(@event.TypeId, @event.Date, @event.Date).Result.ToList();
            EventVenueAvailabilityVM venueSelector = new EventVenueAvailabilityVM(@event, availabilities);
            return View(venueSelector);
        }


        public IActionResult ConfirmReservation(string eventName, TimeSpan duration, string type, string code, DateTime date)
        {
            EventVM eventVM = new EventVM(eventName, date, duration, type);
            EventVenueVM selectedEventVenue = new EventVenueVM(eventVM, code, date);
            return View(selectedEventVenue);
        }

        [HttpPost]
        public async Task<IActionResult> BookEvent([Bind("Code,Date,Title,Duration,TypeId")] FinalBookingVM booking)
        {
            int var = 1;
            return RedirectToAction(nameof(Index));
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
        public async Task<IActionResult> Edit(int? id)
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
            var eventVM = new Models.Events.EventVM(@event);
            return View(eventVM);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Duration")] EditEventVM @event)
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
            client.BaseAddress = new Uri("http://localhost:23652");
            client.DefaultRequestHeaders.Accept.ParseAdd("application/json");
            client.Timeout = TimeSpan.FromSeconds(5);

            IEnumerable<AvailabilitiesVM> availabilities;
            try
            {
                //api / Availability ? eventType = X ? beginDate = X & endDate = X
                //var response = await client.GetAsync("/api/Availability");
                string uri = "/api/Availability";
                string uriEventType = "?eventType=" + eventType;
                string uriBeginDate = "&beginDate=" + beginDate;
                string uriEndDate = "&endDate=" + endDate;
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
    }
}
