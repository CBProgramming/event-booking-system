using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ThAmCo.Events.Data;
using ThAmCo.Events.Models.Customers;

namespace ThAmCo.Events.Controllers
{
    public class CustomersController : Controller
    {
        private readonly EventsDbContext _context;

        public CustomersController(EventsDbContext context)
        {
            _context = context;
        }

        // GET: Customers
        public async Task<IActionResult> Index()
        {
            var customers = await _context.Customers.Where(c => c.Deleted == false).ToListAsync();
            List<CustomerVM> customersVM = new List<CustomerVM>();
            foreach (Customer c in customers)
            {
                customersVM.Add(new CustomerVM(c));
            }
            
            return View(customersVM);
        }

        // GET: Customers/Details/5
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

        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Surname,FirstName,Email")] CustomerVM customerVM)
        {
            if (ModelState.IsValid)
            {
                var customer = new Customer();
                customer.FirstName = customerVM.FirstName;
                customer.Surname = customerVM.Surname;
                customer.Email = customerVM.Email;
                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customerVM);
        }

        // GET: Customers/Edit/5
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

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: Customers/Delete/5
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

        // POST: Customers/Delete/5
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

        private bool CustomerActive(int id)
        {
            return _context.Customers.Any(e => e.Id == id && e.Deleted != true);
        }
    }
}