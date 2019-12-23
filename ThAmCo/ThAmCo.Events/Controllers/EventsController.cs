using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ThAmCo.Catering.Data;
using ThAmCo.Events.Data;
using ThAmCo.Events.Models.Availability;
using ThAmCo.Events.Models.Customers;
using ThAmCo.Events.Models.Events;
using ThAmCo.Events.Models.Staff;
using ThAmCo.Events.Models.Staffing;
using ThAmCo.Events.Models.Venues;
using ThAmCo.Events.Services;
using ThAmCo.Venues.Data;

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
        public async Task<IActionResult> Index(string message)
        {
            var menus = await getMenus();
            List<EventVM> eventsOk = new List<EventVM>();
            List<EventVM> eventsNeedStaff = new List<EventVM>();
            List<EventVM> eventsNeedFirstAid = new List<EventVM>();
            var events = await _context.Events.Where(e => e.IsActive == true).ToListAsync();
            foreach (Event e in events)
            {
                var bookings = await _context.Guests.Include(g => g.Event).Where(g => g.EventId == e.Id).ToListAsync();
                var staffing = await _context.Staffing.Include(g => g.Event).Where(g => g.EventId == e.Id).ToListAsync();
                var firstAidStaff = await _context.Staff.Where(s => s.IsActive == true).Where(t => staffing.Any(b => b.StaffId.Equals(t.Id))).Where(s => s.FirstAider == true).ToListAsync();
                EventVM eventVM = new EventVM(e);
                eventVM.NumGuests = bookings.Count();
                eventVM.NumStaff = staffing.Count();
                if (eventVM.MenuId != 0)
                {
                    MenuDto menu = menus.Where(m => m.MenuId == eventVM.MenuId).FirstOrDefault();
                    eventVM.MenuName = menu.Name;
                }
                if (!eventVM.NeedStaff && firstAidStaff != null && firstAidStaff.Count > 0)
                {
                    eventsOk.Add(eventVM);
                }
                if (eventVM.NeedStaff)
                {
                    eventsNeedStaff.Add(eventVM);
                }
                if (firstAidStaff == null || firstAidStaff.Count == 0)
                {
                    eventsNeedFirstAid.Add(eventVM);
                }
            }
            EventsIndexVM eventLists = new EventsIndexVM(eventsOk, eventsNeedStaff, eventsNeedFirstAid);
            return View(eventLists);
        }

        // GET: Events/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var @event = await _context.Events
                                .Where(e => e.IsActive == true)
                                .FirstOrDefaultAsync(m => m.Id == id);
            if (@event == null)
            {
                return NotFound();
            }
            var eventVM = new EventVM(@event);
            var bookings = _context.Guests.Include(g => g.Customer).Where(g => g.EventId == id);
            var customers = await _context.Customers.Where(c => c.Deleted == false).Where(e => bookings.Any(b => b.CustomerId.Equals(e.Id))).OrderBy(e => e.Id).ToListAsync();
            List<CustomerVM> customersVM = new List<CustomerVM>();
            foreach (Customer c in customers)
            {
                customersVM.Add(new CustomerVM(c));
            }
            var staffing = _context.Staffing.Include(g => g.Staff).Where(g => g.EventId == id);
            var staff = await _context.Staff.Where(e => staffing.Any(b => b.StaffId.Equals(e.Id))).OrderBy(e => e.Id).ToListAsync();
            List<StaffVM> staffVM = new List<StaffVM>();
            foreach (Staff s in staff)
            {
                staffVM.Add(new StaffVM(s));
            }
            MenuDto menuDto = await getMenu(eventVM.MenuId);
            EventDetailsVM eventDetailsVM = new EventDetailsVM(eventVM, customersVM, staffVM, menuDto);
            return View(eventDetailsVM);
        }

        public IActionResult SearchVenues(VenueSearchVM searchCriteria)
        {
            return View(searchCriteria);
        }

        public IActionResult VenueSearchResults([Bind("StartDate,EndDate,TypeId")] VenueSearchVM searchCriteria)
        {
            if (searchCriteria.TypeId == null)
            {
                searchCriteria.Message = "Please select an event type";
                return View("SearchVenues", searchCriteria);
            }
            List<AvailabilitiesVM> availabilities = GetAvailability(searchCriteria.TypeId, searchCriteria.StartDate, searchCriteria.EndDate).Result.ToList();
            if (availabilities.Count == 0)
            {
                searchCriteria.Message = "No venues available on this date";
                return View("SearchVenues", searchCriteria);
            }
            EventVM eventVM = new EventVM();
            eventVM.TypeId = searchCriteria.TypeId;
            EventVenueAvailabilityVM venueSelector = new EventVenueAvailabilityVM(eventVM, availabilities);
            return View("SelectVenue", venueSelector);
        }

        public async Task<IEnumerable<EventTypeDto>> GetEventTypes()
        {
            try
            {
                var client = setupVenueClient();
                string uri = "/api/EventTypes";
                var response = await client.GetAsync(uri);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsAsync<List<EventTypeDto>>();
            }
            catch (Exception e)
            {
            }
            return new List<EventTypeDto>();
        }


        // GET: Events/Create
        public async Task<IActionResult> Create (int day, int month, int year, int hour, int minute, int second, EventVM @event)
        {
            if (day !=0 && month !=0 && year !=0 && @event.Title == null)
            {
                DateTime fixedDate = new DateTime(year, month, day, hour, minute, second);
                @event.Date = fixedDate;
            }
            IEnumerable<EventTypeDto> eventTypes = await GetEventTypes();
            @event.TypeList = new SelectList(eventTypes, "Id", "Title");
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
            if (@event == null || @event.IsActive == false)
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


        public async Task<IActionResult> SelectVenue(int day, int month, int year, int hour, int minute, int second, [Bind("Id,Title,Date,Duration,TypeId,VenueRef,Existing,VenueName,VenueDescription,VenueCapacity,VenueCost,OldRef")] EventVM @event)
        {
            IEnumerable<EventTypeDto> eventTypes = await GetEventTypes();
            @event.Type = eventTypes.First(e => e.Id == @event.TypeId).Title;
            if (day != 0 && month != 0 && year != 0)
            {
                DateTime fixedDate = new DateTime(year, month, day, hour, minute, second);
                @event.Date = fixedDate;
            }
            if (@event.VenueName != null && @event.Existing == false)
            {
                return RedirectToAction("ConfirmReservation", @event);
            }
            else
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

        }

        public IActionResult ConfirmReservation(EventVM eventVM)
        {
            EventVenueVM selectedEventVenue = new EventVenueVM(eventVM, eventVM.VenueRef, eventVM.Date, eventVM.VenueName);
            return View(selectedEventVenue);
        }

        [HttpPost]
        public async Task<IActionResult> BookEvent([Bind("Id,VenueRef,Date,VenueName,VenueDescription,VenueCapacity,VenueCost,Title,Duration,TypeId,Type,Existing,OldRef")] EventVM booking)
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
                    @event.Type = booking.Type;
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
            var @event = await _context.Events.Where(e => e.IsActive == true)
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
            EventVM eventVM = new EventVM(await _context.Events.FindAsync(id));
            var client = setupVenueClient();
            string uri = "/api/Reservations/" + eventVM.getBookingRef;
            ReservationPostDto res = new ReservationPostDto(eventVM.Date, eventVM.VenueRef);
            try
            {
                if ((await client.GetAsync(uri)).IsSuccessStatusCode)
                {
                    var deleteResponse = await client.DeleteAsync(uri);
                    deleteResponse.EnsureSuccessStatusCode();
                }
            }
            catch (Exception)
            {

            }
            List<StaffingVM> staffings = new List<StaffingVM>();
            var staffData = await _context.Staffing.Where(s => s.EventId == id).ToListAsync();
            foreach (Staffing s in staffData)
            {
                staffings.Add(new StaffingVM(s));
            }
            foreach (StaffingVM s in staffings)
            {
                var staffing = await _context.Staffing.FindAsync(s.StaffId,s.EventId);
                _context.Staffing.Remove(staffing);
                await _context.SaveChangesAsync();
            }
            var @event = await _context.Events.FindAsync(id);
            @event.IsActive = false;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventExists(int id)
        {
            return _context.Events.Where(e => e.IsActive == true).Any(e => e.Id == id);
        }



        //IEnumerable<AvailabilityGetDto>
        private async Task<IEnumerable<AvailabilitiesVM>> GetAvailability (string eventType, DateTime beginDate, DateTime endDate)
        {
            var client = setupVenueClient();
            string startDate = stringDate(beginDate);
            string finishDate = stringDate(endDate);
            IEnumerable<AvailabilitiesVM> availabilities;
            try
            {
                string uri = "/api/Availability";
                string uriEventType = "?eventType=" + eventType;
                string uriBeginDate = "&beginDate=" + startDate;
                string uriEndDate = "&endDate=" + finishDate;
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

        public HttpClient setupCateringClient()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(_configuration["MenusBaseURI"]);
            client.DefaultRequestHeaders.Accept.ParseAdd("application/json");
            client.Timeout = TimeSpan.FromSeconds(5);
            return client;
        }


        public async Task<MenuDto> getMenu(int menuId)
        {
            var client = setupCateringClient();
            string uri = "/api/Menu?menuId=" + menuId;
            try
            {
                var response = await client.GetAsync(uri);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsAsync<MenuDto>();
            }
            catch (HttpRequestException e)
            {
            }
            return new MenuDto("Something went wrong, please try again");
        }

        public async Task<IActionResult> menuBrancher (int id)
        {
            var @event = await _context.Events.Where(e => e.IsActive == true).FirstOrDefaultAsync(m => m.Id == id);
            if (@event == null)
            {
                RedirectToAction("Index");
            }
            EventVM @eventVM = new EventVM(@event);
            if (eventVM.MenuId == 0)
            {
                return RedirectToAction("BookMenu", new
                {
                    eventId = id,
                    message = ""
                });
            }
            else
            {
                return RedirectToAction("ViewMenu", new
                {
                    eventId = id,
                    menuId = eventVM.MenuId
                }) ;
            }
        }

        public async Task<IEnumerable<MenuDto>> getMenus()
        {
            var client = setupCateringClient();
            string uri = "/api/Menu";
            List<MenuDto> menus = null;
            try
            {
                var response = await client.GetAsync(uri);
                response.EnsureSuccessStatusCode();
                menus = await response.Content.ReadAsAsync<List<MenuDto>>();
            }
            catch (HttpRequestException e)
            {
            }
            return menus;
        }

        public async Task<IActionResult> BookMenu(int eventId, string message)
        {
            IEnumerable<MenuDto> menus = await getMenus();
            EventMenusVM menuChoices = new EventMenusVM(eventId, menus, message);
            return View(menuChoices);
        }

        public async Task<IActionResult> SelectMenu(int menuId, int eventId)
        {
            FoodBookingDto booking = new FoodBookingDto(menuId, eventId);
            string uri = "/api/FoodBooking";
            var client = setupCateringClient();
            string getUri = uri + "?eventId=" + eventId;
            HttpResponseMessage existingBooking = await client.GetAsync(getUri);
            HttpResponseMessage response;
            if(existingBooking.IsSuccessStatusCode)
            {
                response = await client.PutAsJsonAsync<FoodBookingDto>(uri, booking);
            }
            else
            {
                response = await client.PostAsJsonAsync<FoodBookingDto>(uri, booking);
            }
            if (response.IsSuccessStatusCode)
            {
                Event @event = await _context.Events.Where(e => e.IsActive == true).FirstOrDefaultAsync(e => e.Id == eventId);
                try
                {
                    @event.menuId = menuId;
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {

                }
            }
            return RedirectToAction("BookMenu", new
            {
                eventId,
                message = "Something went wrong, please try again"
            });
        }

        public async Task<IActionResult> ConfirmMenuCancellation(int eventId, int menuId)
        {
            var client = setupCateringClient();
            string uri = "/api/FoodBooking?eventId=" + eventId;

            HttpResponseMessage response = null;
            try
            {
                response = await client.DeleteAsync(uri);
            }
            catch(Exception e)
            {

            }
            if (response.IsSuccessStatusCode)
            {
                Event @event = await _context.Events.Where(e => e.IsActive == true).FirstOrDefaultAsync(e => e.Id == eventId);
                try
                {
                    @event.menuId = 0;
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {

                }
            }
            return RedirectToAction("CancelMenu", new
            {
                eventId,
                menuId,
                message = "Something went wrong, please try again"
            });
        }

        public async Task<IActionResult> CancelMenu(int eventId, int menuId, string message)
        {
            return View(await getEventMenuVM(eventId, menuId,message));
        }

        public async Task<IActionResult> ViewMenu(int eventId, int menuId)
        {
            return View(await getEventMenuVM(eventId, menuId,""));
        }

        public async Task<EventNameAndMenuVM> getEventMenuVM(int eventId, int menuId, string message)
        {
            EventVM @event = new EventVM(await _context.Events.Where(e => e.IsActive == true).FirstOrDefaultAsync(e => e.Id == eventId));
            MenuDto menu = await getMenu(menuId);
            EventNameAndMenuVM eventMenu = new EventNameAndMenuVM(@event, menu);
            return eventMenu;
        }

        public string stringDate(DateTime date)
        {
            string stringDate = date.Month.ToString("00") + "/" + date.Day.ToString("00") + "/" + date.Year.ToString("0000") + " "
                                + date.Hour.ToString("00") + ":" + date.Minute.ToString("00") + ":" + date.Second.ToString("00");
            return stringDate;
        }
    }
}
