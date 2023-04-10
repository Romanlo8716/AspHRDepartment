using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Laba1.Models;
using Microsoft.CodeAnalysis;

namespace Laba1.Controllers
{
    public class MedicalBooksController : Controller
    {
        private readonly AppDBContext _context;

        public MedicalBooksController(AppDBContext context)
        {
            _context = context;
        }

        // GET: MedicalBooks
        public async Task<IActionResult> Index(int? Id)
        {
            Worker worker = await _context.Workers.FindAsync(Id);
            ViewBag.WorkerId = Id;
            ViewBag.Name = worker.Name;
            ViewBag.Surname = worker.Surname;
            ViewBag.Middlename = worker.Middlename;
            ViewBag.Id = Id;

            if (Id == null || _context.LaborBook == null)
            {
                return NotFound();
            }

            var appDBContext = _context.MedicalBook.Include(e => e.Worker).Where(e => Id == e.WorkerId);
            return View(await appDBContext.ToListAsync());
        }

        // GET: MedicalBooks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (User.IsInRole("admin"))
            {
                if (id == null || _context.MedicalBook == null)
                {
                    return NotFound();
                }

                var medicalBook = await _context.MedicalBook.FindAsync(id);

                var workerId = medicalBook.WorkerId;
                Worker worker = await _context.Workers.FindAsync(workerId);
                ViewBag.WorkerId = workerId;
                ViewBag.Name = worker.Name;
                ViewBag.Surname = worker.Surname;
                ViewBag.Middlename = worker.Middlename;

                medicalBook = await _context.MedicalBook
                    .Include(m => m.Worker)
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (medicalBook == null)
                {
                    return NotFound();
                }

                return View(medicalBook);
            }
            else
            {
               return NotFound();
            }
        }

        public IActionResult AddMedicalBook(int? Id)
        {
            if (User.IsInRole("admin"))
            {
                if (Id == null || _context.MedicalBook == null)
                {
                    return NotFound();
                }

                var worker = _context.Workers.Find(Id);
                ViewData["WorkerId"] = Id;


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
        public async Task<IActionResult> AddMedicalBook([Bind("Id,placeExam,dateExam,conclusion,WorkerId")] MedicalBook medicalBook)
        {
            
                int? workerId = medicalBook.WorkerId;
                Worker worker = _context.Workers.Find(workerId);
                ViewBag.WorkerId = workerId;
                if (ModelState.IsValid)
                {
                    _context.Add(medicalBook);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index), new { id = workerId });
                }
                ViewData["WorkerId"] = new SelectList(_context.Workers, "Id", "Middlename", medicalBook.WorkerId);
                return View(medicalBook);
           
        }


        // GET: MedicalBooks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (User.IsInRole("admin"))
            {
                if (id == null || _context.MedicalBook == null)
                {
                    return NotFound();
                }

                var medicalBook = await _context.MedicalBook.FindAsync(id);

                var workerId = medicalBook.WorkerId;
                Worker worker = await _context.Workers.FindAsync(workerId);
                ViewBag.WorkerId = workerId;
                ViewBag.Name = worker.Name;
                ViewBag.Surname = worker.Surname;
                ViewBag.Middlename = worker.Middlename;

                if (medicalBook == null)
                {
                    return NotFound();
                }
                ViewData["WorkerId"] = new SelectList(_context.Workers, "Id", "Middlename", medicalBook.WorkerId);
                return View(medicalBook);
            }
            else
            {
                return NotFound();
            }
        }

        // POST: MedicalBooks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,placeExam,dateExam,conclusion,WorkerId")] MedicalBook medicalBook)
        {
            var workerId = medicalBook.WorkerId;

            if (id != medicalBook.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(medicalBook);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedicalBookExists(medicalBook.Id))
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
            //ViewData["WorkerId"] = new SelectList(_context.Workers, "Id", "Middlename", medicalBook.WorkerId);
            return View(medicalBook);
        }

        // GET: MedicalBooks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (User.IsInRole("admin"))
            {
                if (id == null || _context.MedicalBook == null)
                {
                    return NotFound();
                }

                var medicalBook = await _context.MedicalBook.FindAsync(id);

                var workerId = medicalBook.WorkerId;
                Worker worker = await _context.Workers.FindAsync(workerId);
                ViewBag.WorkerId = workerId;
                ViewBag.Name = worker.Name;
                ViewBag.Surname = worker.Surname;
                ViewBag.Middlename = worker.Middlename;

                medicalBook = await _context.MedicalBook
                    .Include(v => v.Worker)
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (medicalBook == null)
                {
                    return NotFound();
                }

                return View(medicalBook);
            }
            else
            {
                return NotFound();
            }
        }

        // POST: MedicalBooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MedicalBook == null)
            {
                return Problem("Entity set 'AppDBContext.MedicalBooks'  is null.");
            }
            var medicalBook = await _context.MedicalBook.FindAsync(id);
            int? workerId = medicalBook.WorkerId;
            if (medicalBook != null)
            {
                _context.MedicalBook.Remove(medicalBook);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { Id = workerId });
        }

        private bool MedicalBookExists(int id)
        {
          return (_context.MedicalBook?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
