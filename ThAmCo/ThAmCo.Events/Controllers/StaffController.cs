using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThAmCo.Events.Data;
using ThAmCo.Events.Models.Staff;

namespace ThAmCo.Events.Controllers
{
    public class StaffController : Controller
    {
        private readonly EventsDbContext _context;

        public StaffController(EventsDbContext context)
        {
            _context = context;
        }

        // GET: Staff
        public async Task<IActionResult> Index()
        {
            var staff = await _context.Staff.ToListAsync();
            List<StaffVM> staffVM = new List<StaffVM>();
            foreach (Staff s in staff)
            {
                staffVM.Add(new StaffVM(s));
            }
            return View(staffVM);
        }

        // GET: Staff/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            StaffVM staff = new StaffVM(await _context.Staff.FirstOrDefaultAsync(m => m.Id == id));
            if (staff == null)
            {
                return NotFound();
            }

            return View(staff);
        }

        // GET: Staff/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // POST: Staff/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Surname,FirstName,Email,FirstAider,IsActive")] StaffVM staffVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Staff staff = new Staff();
                    staff.Surname = staffVM.Surname;
                    staff.FirstName = staffVM.FirstName;
                    staff.Email = staffVM.Email;
                    staff.FirstAider = staffVM.FirstAider;
                    staff.IsActive = true;
                    _context.Add(staff);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    staffVM.Message = "Something went wrong.  Please ensure all fields are completed and try again";
                    return View(staffVM);
                }
            }
            return View(staffVM);
        }

        // GET: Staff/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            StaffVM staff = new StaffVM(await _context.Staff.FirstOrDefaultAsync(m => m.Id == id));
            return View(staff);
        }

        // POST: Staff/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id,Surname,FirstName,Email,FirstAider,IsActive")] StaffVM staffVM)
        {
            if (staffVM == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    Staff staff = await _context.Staff.FirstOrDefaultAsync(m => m.Id == staffVM.Id);
                    staff.Surname = staffVM.Surname;
                    staff.FirstName = staffVM.FirstName;
                    staff.Email = staffVM.Email;
                    staff.FirstAider = staffVM.FirstAider;
                    staff.IsActive = staffVM.IsActive;
                    _context.Update(staff);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StaffExists(staffVM.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        staffVM.Message = "Something went wrong.  Please ensure all fields are completed and try again";
                        return View(staffVM);
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(staffVM);
        }

        // GET: Staff/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            StaffVM staff = new StaffVM(await _context.Staff.FirstOrDefaultAsync(m => m.Id == id));
            if (staff == null)
            {
                return NotFound();
            }

            return View(staff);
        }

        // POST: Staff/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var staff = await _context.Staff.FindAsync(id);
            _context.Staff.Remove(staff);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StaffExists(int id)
        {
            return _context.Staff.Any(e => e.Id == id);
        }
    }
}
