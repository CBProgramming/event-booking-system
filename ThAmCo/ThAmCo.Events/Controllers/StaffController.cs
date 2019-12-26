using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThAmCo.Events.Data;
using ThAmCo.Events.Models.Staff;

namespace ThAmCo.Events.Controllers
{

    //Staff controller to manage staff CRUD
    //View models used throughout to separate processes from backend database
    public class StaffController : Controller
    {
        private readonly EventsDbContext _context;

        public StaffController(EventsDbContext context)
        {
            _context = context;
        }

        //Returns view of list of staff
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

        //Returns view of staff details, based on staffid provided
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

        //Returns view used to create new staff record
        public async Task<IActionResult> Create()
        {
            return View();
        }

        //Creates new staff record, based on staff view model provided
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

        //Returns view of staff record which can be edited, based on staff id provided
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            StaffVM staff = new StaffVM(await _context.Staff.FirstOrDefaultAsync(m => m.Id == id));
            return View(staff);
        }

        //Updates existing staff record, based on staff view model provided
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

        //Returns confirmation view of deleting staff record, based on staff id provided
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

        //Deletes staff record, based on staff id provided
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var staff = await _context.Staff.FindAsync(id);
            _context.Staff.Remove(staff);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //Reusable method to check if staff record exists
        private bool StaffExists(int id)
        {
            return _context.Staff.Any(e => e.Id == id);
        }
    }
}
