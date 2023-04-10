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
    public class LaborBooksController : Controller
    {
        private readonly AppDBContext _context;

        public LaborBooksController(AppDBContext context)
        {
            _context = context;
        }

        // GET: LaborBooks
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
            var appDBContext = _context.LaborBook.Include(e => e.Worker).Where(e => Id == e.WorkerId);
            return View(await appDBContext.ToListAsync());
        }

        // GET: LaborBooks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (User.IsInRole("admin"))
            {
                if (id == null || _context.LaborBook == null)
                {
                    return NotFound();
                }

                var laborBook = await _context.LaborBook.FindAsync(id);

                var workerId = laborBook.WorkerId;
                Worker worker = await _context.Workers.FindAsync(workerId);
                ViewBag.WorkerId = workerId;
                ViewBag.Name = worker.Name;
                ViewBag.Surname = worker.Surname;
                ViewBag.Middlename = worker.Middlename;


                laborBook = await _context.LaborBook
                   .Include(v => v.Worker)
                   .FirstOrDefaultAsync(m => m.Id == id);
                if (laborBook == null)
                {
                    return NotFound();
                }

                return View(laborBook);
            }
            else
            {
                return NotFound();
            }
        }

        public IActionResult AddLaborBook(int? Id)
        {
            if (User.IsInRole("admin"))
            {
                if (Id == null || _context.LaborBook == null)
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
        public async Task<IActionResult> AddLaborBook([Bind("Id,dateRecord,nameWork,intelligenceWork, nameDocument,numberDocument,WorkerId")] LaborBook laborBook)
        {
            int? workerId = laborBook.WorkerId;
            Worker worker = _context.Workers.Find(workerId);
            ViewBag.WorkerId = workerId;
            if (ModelState.IsValid)
            {
                _context.Add(laborBook);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { id = workerId });
            }
            ViewData["WorkerId"] = new SelectList(_context.Workers, "Id", "Middlename", laborBook.WorkerId);
            return View(laborBook);
        }

        // GET: LaborBooks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (User.IsInRole("admin"))
            {
                if (id == null || _context.LaborBook == null)
                {
                    return NotFound();
                }

                var laborBook = await _context.LaborBook.FindAsync(id);

                var workerId = laborBook.WorkerId;
                Worker worker = await _context.Workers.FindAsync(workerId);
                ViewBag.WorkerId = workerId;
                ViewBag.Name = worker.Name;
                ViewBag.Surname = worker.Surname;
                ViewBag.Middlename = worker.Middlename;


                if (laborBook == null)
                {
                    return NotFound();
                }

                return View(laborBook);
            }
            else
            {
                return NotFound();
            }
        }

        // POST: LaborBooks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,dateRecord,nameWork,intelligenceWork,nameDocument,numberDocument,WorkerId")] LaborBook laborBook)
        {
            var workerId = laborBook.WorkerId;

            if (id != laborBook.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(laborBook);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LaborBookExists(laborBook.Id))
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
            return View(laborBook);
        }

        // GET: LaborBooks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (User.IsInRole("admin"))
            {
                if (id == null || _context.LaborBook == null)
                {
                    return NotFound();
                }

                var laborBook = await _context.LaborBook.FindAsync(id);

                var workerId = laborBook.WorkerId;
                Worker worker = await _context.Workers.FindAsync(workerId);




                laborBook = await _context.LaborBook
                    .Include(v => v.Worker)
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (laborBook == null)
                {
                    return NotFound();
                }

                return View(laborBook);
            }
            else
            {
                return NotFound();
            }
        }

        // POST: LaborBooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.LaborBook == null)
            {
                return Problem("Entity set 'AppDBContext.LaborBooks'  is null.");
            }
            var laborBook = await _context.LaborBook.FindAsync(id);
            int? workerId = laborBook.WorkerId;
            if (laborBook != null)
            {
                _context.LaborBook.Remove(laborBook);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { Id = workerId });
        }

        private bool LaborBookExists(int id)
        {
          return (_context.LaborBook?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
