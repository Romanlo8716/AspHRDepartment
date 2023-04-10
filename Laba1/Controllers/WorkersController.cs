using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Laba1.Models;
using Laba1.Models.ViewModels;

namespace Laba1.Controllers
{
    public class WorkersController : Controller
    {
        private readonly AppDBContext _context;

        public WorkersController(AppDBContext context)
        {
            _context = context;
        }

        // GET: Workers
        public async Task<IActionResult> Index()
        {
            var publishingDBContext = _context.Workers;

            return View(await publishingDBContext.ToListAsync());
            //return View(await _context.Workers.ToListAsync());
        }

        // GET: Workers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Workers == null)
            {
                return NotFound();
            }

            var worker = await _context.Workers.FindAsync(id);
            if (worker == null)
            {
                return NotFound();
            }

            return View(worker);
        }

        public async Task<IActionResult> Intelligence(int? id)
        {
            ViewBag.workerId = id; 

            if (id == null || _context.Workers == null)
            {
                return NotFound();
            }

            var worker = await _context.Workers.FirstOrDefaultAsync(m => m.Id == id);

            DescriptionsWorker descWorker = new DescriptionsWorker();
            descWorker.worker = worker;
            descWorker.laborBooks = _context.LaborBook.Include(e => e.Worker).Where(e => id == e.WorkerId);
            descWorker.educations = _context.Educations.Include(e => e.Worker).Where(e => id == e.WorkerId);
            descWorker.vacations = _context.Vacations.Include(e => e.Worker).Where(e => id == e.WorkerId);
            descWorker.medicalBooks = _context.MedicalBook.Include(e => e.Worker).Where(e => id == e.WorkerId);
            //descWorker.departmentsOfWorkers = _context.DepartmentsOfWorker.Include(e => e.Worker).Include(e => e.Department).Where(e => id == e.WorkerId);
            //descWorker.postsOfWorkers = _context.PostsOfWorker.Include(e => e.Worker).Include(e => e.Post).Where(e => id == e.WorkerId);

            descWorker.departmentsAndPostsOfWorkers = _context.DepartmentsAndPostsOfWorker.Include(e => e.Worker).Include(e => e.Department).Include(e => e.Post).Where(e => id == e.WorkerId);
            
            if (worker == null)
            {
                return NotFound();
            }

            ViewData["Department"] = new SelectList(_context.Departments, "Id", "Name", worker.Id);

            return View(descWorker);
        }


     
       

        // GET: Workers/Create
        public IActionResult Create()
        {
            if (User.IsInRole("admin"))
            {
              
                return View();
            }
            else
            {
                
                return NotFound();
            }
                
        }

        // POST: Workers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Surname,Middlename,Phone,Gender")] Worker worker)
        {
            if (ModelState.IsValid)
            {
                _context.Add(worker);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
           
            return View(worker);
        }

        // GET: Workers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (User.IsInRole("admin"))
            {
                if (id == null || _context.Workers == null)
                {
                    return NotFound();
                }

                var worker = await _context.Workers.FindAsync(id);
                if (worker == null)
                {
                    return NotFound();
                }
                
                return View(worker);
            }
            else
            {
                return NotFound();
            }
        }

        // POST: Workers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Surname,Middlename,Phone,Gender,idPost,idDepartment")] Worker worker)
        {
            if (id != worker.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(worker);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkerExists(worker.Id))
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
           
            return View(worker);
        }

        // GET: Workers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (User.IsInRole("admin"))
            {
                if (id == null || _context.Workers == null)
                {
                    return NotFound();
                }


                var worker = await _context.Workers.FirstOrDefaultAsync(m => m.Id == id);
                if (worker == null)
                {
                    return NotFound();
                }

                return View(worker);
            }
            else
            {
                return NotFound();
            }
        }

        // POST: Workers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Workers == null)
            {
                return Problem("Entity set 'AppDBContext.Workers'  is null.");
            }
            var worker = await _context.Workers.FindAsync(id);
            if (worker != null)
            {
                _context.Workers.Remove(worker);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorkerExists(int id)
        {
          return _context.Workers.Any(e => e.Id == id);
        }
    }
}
