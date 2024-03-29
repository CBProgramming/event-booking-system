﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThAmCo.Events.Data;
using ThAmCo.Events.Models.Customers;

namespace ThAmCo.Events.Controllers
{
    //Customers controller to manage customer CRUD
    //View models used throughout instead of data models to separate processes from backend database
    //Entire solution tested and functional on Teesside University lab PC 42823
    public class CustomersController : Controller
    {
        private readonly EventsDbContext _context;

        public CustomersController(EventsDbContext context)
        {
            _context = context;
        }

        //Returns view of list of all existing customers
        public async Task<IActionResult> Index()
        {
            var customers = await _context.Customers.Where(c => c.Deleted == false).OrderBy(c => c.Surname).ToListAsync();
            List<CustomerVM> customersVM = new List<CustomerVM>();
            foreach (Customer c in customers)
            {
                customersVM.Add(new CustomerVM(c));
            }
            
            return View(customersVM);
        }

        //Returns view of details of existing customer based on customer id provided
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var customer = await _context.Customers.Where(c => c.Deleted == false).FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }
            var customerVM = new Models.Customers.CustomerVM(customer);
            return View(customerVM);
        }

        //Returns view used to create new customer
        public IActionResult Create()
        {
            return View();
        }

        //Creates new customer record based on customer view model provided
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Surname,FirstName,Email")] CustomerVM customerVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var customer = new Customer();
                    customer.FirstName = customerVM.FirstName;
                    customer.Surname = customerVM.Surname;
                    customer.Email = customerVM.Email;
                    _context.Add(customer);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    // view model error message var used instead of view bag
                    customerVM.Message = "Something went wrong.  Please ensure all fields are complete and try again";
                }
            }
            return View(customerVM);
        }

        //Returns view of customer details to be edited by user
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var customer = await _context.Customers.Where(c => c.Deleted == false).FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }
            var customerVM = new Models.Customers.CustomerVM(customer);
            return View(customerVM);
        }

        //Edits customer details held in database, based on customer view model provided
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Surname,FirstName,Email")] CustomerVM customerVM)
        {
            if (id != customerVM.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                if (!CustomerActive(customerVM.Id))
                {
                    return NotFound();
                }
                try
                {
                    var customer = await _context.Customers.Where(c => c.Deleted == false).FirstOrDefaultAsync(m => m.Id == id);
                    customer.FirstName = customerVM.FirstName;
                    customer.Surname = customerVM.Surname;
                    customer.Email = customerVM.Email;
                    await _context.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    customerVM.Message = "Something went wrong, ensure all fields are completed and try again";
                    return View(customerVM);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(customerVM);
        }

        // Returns confirmation page with customer details prior to customer deletion
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            CustomerVM customerVM = new CustomerVM(await _context.Customers.Where(c => c.Deleted == false).FirstOrDefaultAsync(m => m.Id == id));
            if (customerVM == null)
            {
                return NotFound();
            }
            return View(customerVM);
        }

        //Soft deletes customer record from database based on id provided by setting customer.deleted 
        //flag to true and anonymising their personal data
        //Logic in other methods throughout solution checks for this to filter deleted customers
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                var customer = await _context.Customers.FindAsync(id);
                if (customer.Deleted != true)
                {
                    customer.Deleted = true;
                    customer.FirstName = "anonymised";
                    customer.Surname = "anonymised";
                    customer.Email = "anonymised@anonymised.com";
                    await _context.SaveChangesAsync();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerActive(id))
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

        //Method returning boolean as to whether customer is active or soft deleted
        private bool CustomerActive(int id)
        {
            return _context.Customers.Any(e => e.Id == id && e.Deleted != true);
        }
    }
}