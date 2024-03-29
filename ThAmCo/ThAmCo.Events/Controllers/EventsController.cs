﻿using System;
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
    //Events controller to manage event CRUD
    //View models used instead of data models throughout to separate processes from backend database
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
        // Loaded as main screen for best user experience
        // Creates three lists of events, those requiring more staff, those requiring a first aider 
        //and those with enough staff
        public async Task<IActionResult> Index(string message)
        {
            var menus = await getMenus();
            List<EventVM> eventsOk = new List<EventVM>();
            List<EventVM> eventsNeedStaff = new List<EventVM>();
            List<EventVM> eventsNeedFirstAid = new List<EventVM>();
            var events = await _context.Events.Where(e => e.IsActive == true).OrderBy(e => e.Title).ToListAsync();
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
                // view model getter methods used to easily determine if staff and/or first aiders are needed
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
            // custom view models used where appropritate display information as required in brief
            EventsIndexVM eventLists = new EventsIndexVM(eventsOk, eventsNeedStaff, eventsNeedFirstAid);
            return View(eventLists);
        }

        // GET: Events/Edit/5
        // Loads event data based on Id and allows title and duration to be edited
        // Also shows venue details with a link to edit to increase usability
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
            return View(eventVM);
        }

        // POST: Events/Edit/5
        // Updates event details in database based on user input
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Date,Duration,TypeId,VenueRef,Existing,VenueName,VenueDescription,VenueCapacity,VenueCost,OldRef")] EventVM @event)
        {
            if(@event.Duration.HasValue && (@event.Duration).Value.TotalDays >= 1)
            {
                // view model error message var used instead of view bag
                @event.Message = "Duration must be less than 24 hours.  Please enter as HH:MM or HH:MM:SS";
                return View(@event);
            }
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

        // GET: Events/Details/5
        // Loads event details based on event id with full breakdown of event, venue, menu
        // costing and all guests and staff
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
            var customers = await _context.Customers.Where(c => c.Deleted == false).Where(e => bookings.Any(b => b.CustomerId.Equals(e.Id))).OrderBy(c => c.Surname).ToListAsync();
            List<CustomerVM> customersVM = new List<CustomerVM>();
            foreach (Customer c in customers)
            {
                customersVM.Add(new CustomerVM(c));
            }
            var staffing = _context.Staffing.Include(g => g.Staff).Where(g => g.EventId == id);
            var staff = await _context.Staff.Where(e => staffing.Any(b => b.StaffId.Equals(e.Id))).OrderBy(s => s.Surname).ToListAsync();
            List<StaffVM> staffVM = new List<StaffVM>();
            foreach (Staff s in staff)
            {
                staffVM.Add(new StaffVM(s));
            }
            MenuDto menuDto = await getMenu(eventVM.MenuId);
            // view model of view models used to pass multiple self contained groups of data to a customised view as required by the brief
            // instead of creating one view model which contains every field of data requited
            EventDetailsVM eventDetailsVM = new EventDetailsVM(eventVM, customersVM, staffVM, menuDto);
            return View(eventDetailsVM);
        }

        // GET: Events/Create
        // Initiates process for creating a new event, part of a multi route process allowing users
        // to set up an event then select venue, or filter venues and then create event
        // Date fix implemented as days and months are switched incorrectly in parts of this multi route process
        public async Task<IActionResult> Create(int day, int month, int year, int hour, int minute, int second, EventVM @event)
        {
            if (day != 0 && month != 0 && year != 0 && @event.Title == null)
            {
                DateTime fixedDate = new DateTime(year, month, day, hour, minute, second);
                @event.Date = fixedDate;
            }
            if (@event.Type == null || @event.TypeList == null)
            {
                IEnumerable<EventTypeDto> eventTypes = await GetEventTypes();
                @event.TypeList = new SelectList(eventTypes, "Id", "Title");
            }
            return View(@event);
        }

        // GET: Events/Select Venue
        //Second stage of create event process, displaying events available on date provided 
        public async Task<IActionResult> SelectVenue(int day, int month, int year, int hour, int minute, int second, [Bind("Id,Title,Date,Duration,TypeId,VenueRef,Existing,VenueName,VenueDescription,VenueCapacity,VenueCost,OldRef")] EventVM @event)
        {
            // multiple if statements ensure correct braching and catching or errors simultaneously
            // instead of having one route through the entire system
            if (@event == null)
            {
                @event.Message = "Something went wrong.  Please ensure all fields are completed and try again.";
                return RedirectToAction("SelectVenue", @event);
            }
            if (@event.Duration.HasValue && (@event.Duration).Value.TotalDays >= 1)
            {
                @event.Message = "Duration must be less than 24 hours.  Please enter as HH:MM or HH:MM:SS";
                return RedirectToAction("Create", @event);
            }
            if (day != 0 && month != 0 && year != 0)
            {
                DateTime fixedDate = new DateTime(year, month, day, hour, minute, second);
                @event.Date = fixedDate;
            }
            if (@event.Type == null)
            {
                IEnumerable<EventTypeDto> eventTypes = await GetEventTypes();
                @event.Type = eventTypes.First(e => e.Id == @event.TypeId).Title;
            }

            if (@event.VenueName != null && @event.Existing == false)
            {
                return RedirectToAction("ConfirmReservation", @event);
            }
            else
            {
                if (@event.Existing)
                    // OldRef variable used to store the old referance when passinbg between views and actions
                    //a llowing new reference to be stored without losing old reference which may be needed when accessing Venues API later in process 
                    @event.OldRef = @event.getBookingRef;
                @event.OldRef = @event.getBookingRef;
                List<AvailabilitiesVM> availabilities = GetAvailability(@event.TypeId, @event.Date, @event.Date).Result.ToList();
                if (availabilities.Count == 0)
                {
                    @event.Message = "No venues available on this date";
                    if (@event.Existing == false)
                        return RedirectToAction("Create", @event);
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


        //Confirmation page allowing user to review event/venue information before confirming booking
        public IActionResult ConfirmReservation(EventVM eventVM)
        {
            EventVenueVM selectedEventVenue = new EventVenueVM(eventVM, eventVM.VenueRef, eventVM.Date, eventVM.VenueName);
            return View(selectedEventVenue);
        }

        //Posts selected event and venue information to local database and creates booking in Venues API
        [HttpPost]
        public async Task<IActionResult> BookEvent([Bind("Id,VenueRef,Date,VenueName,VenueDescription,VenueCapacity,VenueCost,Title,Duration,TypeId,Type,Existing,OldRef")] EventVM booking)
        {
            var client = setupVenueClient();
            string uri = "/api/Reservations";
            string uriOldRef = uri + "/" + booking.OldRef;
            ReservationPostDto res = new ReservationPostDto(booking.Date, booking.VenueRef);
            try
            {
                if ((await client.GetAsync(uriOldRef)).IsSuccessStatusCode)
                {
                    var deleteResponse = await client.DeleteAsync(uriOldRef);
                    deleteResponse.EnsureSuccessStatusCode();
                }
                var postResponse = await client.PostAsJsonAsync(uri, res);
                postResponse.EnsureSuccessStatusCode();
                if (booking.Existing)  // need the ID!
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
                    // all relevant venue data saved locally to prevent the alternative of having to query the API constantly to display venue information
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

        //Secondary event creation process initiator, allowing users so search venues
        //using date range before creating event
        public async Task<IActionResult> SearchVenues(VenueSearchVM searchCriteria)
        {
            IEnumerable<EventTypeDto> eventTypes = await GetEventTypes();
            searchCriteria.TypeList = new SelectList(eventTypes, "Id", "Title");
            return View(searchCriteria);
        }

        //Search results for events in selected date range in secondary event creation route
        public async Task<IActionResult> VenueSearchResults([Bind("StartDate,EndDate,TypeId,Type")] VenueSearchVM searchCriteria)
        {
            IEnumerable<EventTypeDto> eventTypes = await GetEventTypes();
            if (searchCriteria.TypeList == null)
            {
                searchCriteria.TypeList = new SelectList(eventTypes, "Id", "Title");
            }
            if (searchCriteria.Type == null || searchCriteria.TypeList == null)
            {
                searchCriteria.Type = eventTypes.First(e => e.Id == searchCriteria.TypeId).Title;
            }
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

        //Accesses venues API to get a list of all event types to be used in select lists in event creation processes
        //Ensures event type lists are always up to date
        public async Task<IEnumerable<EventTypeDto>> GetEventTypes()
        {
            try
            {
                var client = setupVenueClient();
                string uri = "/api/EventTypes";
                var response = await client.GetAsync(uri);
                response.EnsureSuccessStatusCode();
                return (await response.Content.ReadAsAsync<List<EventTypeDto>>()).OrderBy(e => e.Title);
            }
            catch (Exception e)
            {
            }
            return new List<EventTypeDto>();
        }

        // Initiates event deletion process, based on event id
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
            var eventVM = new EventVM(@event);
            return View(eventVM);
        }

        // Posts to Venues API to cancel venue booking, allowing it to be booked to another event
        // Removes all staff allocated to the event
        // Sets event.active flag to false in local db to act as soft delete, keeping all bookings if needed
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

        // reusable method to check if an event already exists
        private bool EventExists(int id)
        {
            return _context.Events.Where(e => e.IsActive == true).Any(e => e.Id == id);
        }

        //Reusable method to get all available venues from Venues API, based on date range and event type
        // Allows multiple routes though the site to be coded without having to recode this query each time
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
                availabilities = Array.Empty<AvailabilitiesVM>();
            }
            return availabilities;
        }

        //Reusable method to set up Http client for venues API
        private HttpClient setupVenueClient()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(_configuration["VenuesBaseURI"]);
            client.DefaultRequestHeaders.Accept.ParseAdd("application/json");
            client.Timeout = TimeSpan.FromSeconds(5);
            return client;
        }

        //Reusable method to set up Http client for catering API
        public HttpClient setupCateringClient()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(_configuration["MenusBaseURI"]);
            client.DefaultRequestHeaders.Accept.ParseAdd("application/json");
            client.Timeout = TimeSpan.FromSeconds(5);
            return client;
        }

        //Accesses catering API and gets menu based on menu id
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

        //Method used to determine which route to take when accessing menu link via events
        //dependant on if a menu is already booked to the event
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

        //Reusable method which gets all available menus from catering API
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
            return menus.OrderBy(m => m.Name);
        }

        //Displays a list of menus to the user to select which to book to event
        public async Task<IActionResult> BookMenu(int eventId, string message)
        {
            IEnumerable<MenuDto> menus = await getMenus();
            EventMenusVM menuChoices = new EventMenusVM(eventId, menus, message);
            return View(menuChoices);
        }

        //Posts/puts chosen menu to catering API and saves a menu id to local database for reference
        //Unlike venue information, menu data stored locally is minimal to prevent too much unecessary data duplication
        //and to ensure menu info is always queries from the API to ensure it is up to date
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

        // Cancel menu screen to enusure user wishes to cancel menu, for better user experience
        // Alternative to simply cancelling without confirmation
        public async Task<IActionResult> CancelMenu(int eventId, int menuId, string message)
        {
            return View(await getEventMenuVM(eventId, menuId, message));
        }

        //Accesses catering API to cancel menu booking and also removes local reference
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

        //Displays currently booked menu for specific event to user
        public async Task<IActionResult> ViewMenu(int eventId, int menuId)
        {
            return View(await getEventMenuVM(eventId, menuId,""));
        }

        //Reusable method which accesses catering API and returns menu details
        public async Task<EventNameAndMenuVM> getEventMenuVM(int eventId, int menuId, string message)
        {
            EventVM @event = new EventVM(await _context.Events.Where(e => e.IsActive == true).FirstOrDefaultAsync(e => e.Id == eventId));
            MenuDto menu = await getMenu(menuId);
            EventNameAndMenuVM eventMenu = new EventNameAndMenuVM(@event, menu);
            return eventMenu;
        }

        //Method used for date fixing due to bug which flips the day and month
        //Source of bug to be investigated, time allowing
        public string stringDate(DateTime date)
        {
            string stringDate = date.Month.ToString("00") + "/" + date.Day.ToString("00") + "/" + date.Year.ToString("0000") + " "
                                + date.Hour.ToString("00") + ":" + date.Minute.ToString("00") + ":" + date.Second.ToString("00");
            return stringDate;
        }
    }
}
