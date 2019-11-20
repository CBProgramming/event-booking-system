﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ThAmCo.Events.Services;

namespace ThAmCo.Events.Controllers
{
    public class AvailabilityController : Controller
    {
        private readonly ILogger<AvailabilityController> _logger;

        public AvailabilityController(ILogger<AvailabilityController> logger)
        {
            _logger = logger;
        }

        // GET: Availability
        public async Task<ActionResult> Index()
        {
            //declaring fake variables, need to pass these in as parameters


            var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:23652");
            client.DefaultRequestHeaders.Accept.ParseAdd("application/json");
            client.Timeout = TimeSpan.FromSeconds(5);

            IEnumerable<AvailabilityGetDto> availabilities = null;
            try
            {
                //api / Availability ? eventType = X ? beginDate = X & endDate = X
                //var response = await client.GetAsync("/api/Availability");
                string eventType = "CON";
                DateTime beginDate = new DateTime(2018, 12, 03);
                DateTime endDate = new DateTime(2021, 11, 04);
                string uri = "/api/Availability";
                string uriEventType = "?eventType=" + eventType;
                string uriBeginDate = "&beginDate=" + beginDate;
                string uriEndDate = "&endDate=" + endDate;
                //var response = await client.GetAsync(uri + uriEventType + uriBeginDate + uriEndDate);
                var response = await client.GetAsync(uri + uriEventType + uriEndDate + uriBeginDate);
                response.EnsureSuccessStatusCode();
                availabilities = await response.Content.ReadAsAsync<IEnumerable<AvailabilityGetDto>>();
            }
            catch (HttpRequestException e)
            {
                _logger.LogError("Bad response from Availabilities");
                availabilities = Array.Empty<AvailabilityGetDto>();
            }

            return View(availabilities.ToList());
        }

        // GET: Availability/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Availability/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Availability/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Availability/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Availability/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Availability/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Availability/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}