using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Laba1.Models;

namespace Laba1.Controllers
{
    public class VacationsController : Controller
    {
        private readonly AppDBContext _context;

        public VacationsController(AppDBContext context)
        {
            _context = context;
        }

        // GET: Vacations
        public async Task<IActionResult> Index(int? Id)
        {
            Worker worker = await _context.Workers.FindAsync(Id);
            ViewBag.WorkerId = Id;
            ViewBag.Name = worker.Name;
            ViewBag.Surname = worker.Surname;
            ViewBag.Middlename = worker.Middlename;
            ViewBag.Id = Id;

            if (Id == null || _context.Vacations == null)
            {
                return NotFound();
            }
            var appDBContext = _context.Vacations.Include(e => e.Worker).Where(e => Id == e.WorkerId);
            return View(await appDBContext.ToListAsync());
        }

        // GET: Vacations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (User.IsInRole("admin"))
            {
                if (id == null || _context.Vacations == null)
                {
                    return NotFound();
                }

                var vacation = await _context.Vacations.FindAsync(id);

                var workerId = vacation.WorkerId;
                Worker worker = await _context.Workers.FindAsync(workerId);
                ViewBag.WorkerId = workerId;
                ViewBag.Name = worker.Name;
                ViewBag.Surname = worker.Surname;
                ViewBag.Middlename = worker.Middlename;


                vacation = await _context.Vacations
                   .Include(v => v.Worker)
                   .FirstOrDefaultAsync(m => m.Id == id);
                if (vacation == null)
                {
                    return NotFound();
                }

                return View(vacation);
            }
            else
            {
                return NotFound();
            }
        }

        // GET: Vacations/Create
       

        public IActionResult AddVacation(int? Id)
        {
            if (User.IsInRole("admin"))
            {
                if (Id == null || _context.Vacations == null)
                {
                    return NotFound();
                }

                var worker = _context.Workers.Find(Id);
                ViewData["WorkerId"] = Id;

                //var education = _context.Educations.Find();
                ViewBag.WorkerId = Id;
                ViewBag.Name = worker.Name;
                ViewBag.Surname = worker.Surname;
                ViewBag.Middlename = worker.Middlename;
                ViewBag.Id = Id;

                return View();
            }
            else
            {
                return NotFound();
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddVacation([Bind("Id,dateStart,dateEnd,typeVacation, WorkerId")] Vacation vacation)
        {
            int? workerId = vacation.WorkerId;
            Worker worker = _context.Workers.Find(workerId);
            ViewBag.WorkerId = workerId;
            if (ModelState.IsValid)
            {
                _context.Add(vacation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { id =  workerId});
            }
            ViewData["WorkerId"] = new SelectList(_context.Workers, "Id", "Middlename", vacation.WorkerId);
            return View(vacation);
        }

        // GET: Vacations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (User.IsInRole("admin"))
            {
                if (id == null || _context.Vacations == null)
                {
                    return NotFound();
                }

                var vacation = await _context.Vacations.FindAsync(id);

                var workerId = vacation.WorkerId;
                Worker worker = await _context.Workers.FindAsync(workerId);
                ViewBag.WorkerId = workerId;
                ViewBag.Name = worker.Name;
                ViewBag.Surname = worker.Surname;
                ViewBag.Middlename = worker.Middlename;


                if (vacation == null)
                {
                    return NotFound();
                }

                return View(vacation);
            }
            else
            {
                return NotFound();
            }
        }

        // POST: Vacations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,dateStart,dateEnd,typeVacation,WorkerId")] Vacation vacation)
        {

            var workerId = vacation.WorkerId;

            if (id != vacation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vacation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VacationExists(vacation.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { Id = workerId });
            }
            //ViewData["WorkerId"] = new SelectList(_context.Workers, "Id", "Middlename", vacation.WorkerId);
            return View(vacation);
        }

        // GET: Vacations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (User.IsInRole("admin"))
            {
                if (id == null || _context.Vacations == null)
                {
                    return NotFound();
                }

                var vacation = await _context.Vacations.FindAsync(id);

                var workerId = vacation.WorkerId;
                Worker worker = await _context.Workers.FindAsync(workerId);
                ViewBag.WorkerId = workerId;
                ViewBag.Name = worker.Name;
                ViewBag.Surname = worker.Surname;
                ViewBag.Middlename = worker.Middlename;


                vacation = await _context.Vacations
                    .Include(v => v.Worker)
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (vacation == null)
                {
                    return NotFound();
                }

                return View(vacation);
            }
            else
            {
                return NotFound();
            }
        }

        // POST: Vacations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Vacations == null)
            {
                return Problem("Entity set 'AppDBContext.Vacations'  is null.");
            }
            var vacation = await _context.Vacations.FindAsync(id);
            int? workerId = vacation.WorkerId;
            if (vacation != null)
            {
                _context.Vacations.Remove(vacation);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { Id = workerId });
        }

        private bool VacationExists(int id)
        {
          return (_context.Vacations?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
