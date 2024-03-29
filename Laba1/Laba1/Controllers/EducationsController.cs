﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Laba1.Models;

namespace Laba1.Controllers
{
    public class EducationsController : Controller
    {
        private readonly AppDBContext _context;

        public EducationsController(AppDBContext context)
        {
            _context = context;
        }

        // GET: Educations
        public async Task<IActionResult> Index(int? Id)
        {
            Worker worker = await _context.Workers.FindAsync(Id);
            ViewBag.WorkerId = Id;
            ViewBag.Name = worker.Name;
            ViewBag.Surname = worker.Surname;
            ViewBag.Middlename = worker.Middlename;
            ViewBag.Id = Id;


            if (Id == null || _context.Educations == null)
            {
                return NotFound();
            }
            var appDBContext = _context.Educations.Include(e => e.Worker).Where(e => Id == e.WorkerId);
            return View(await appDBContext.ToListAsync());
        }

        // GET: Educations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Educations == null)
            {
                return NotFound();
            }

            var education = await _context.Educations.FindAsync(id);

            var workerId = education.WorkerId;
            Worker worker = await _context.Workers.FindAsync(workerId);
            ViewBag.WorkerId = workerId;
            ViewBag.Name = worker.Name;
            ViewBag.Surname = worker.Surname;
            ViewBag.Middlename = worker.Middlename;
            ViewBag.yearEnd = education.yearEnd;

            education = await _context.Educations
                .Include(e => e.Worker)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (education == null)
            {
                return NotFound();
            }

            return View(education);
        }

        // GET: Educations/Create
        public IActionResult Create()
        {
            ViewData["WorkerId"] = new SelectList(_context.Workers, "Id", "Middlename");
            return View();
        }
        
        // POST: Educations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,diplomSeries,diplomNumber,special,yearEnd,WorkerId")] Education education)
        {
            int? workerId = education.WorkerId;
            Worker worker = _context.Workers.Find(workerId);
            ViewBag.WorkerId = workerId;
            //ViewBag.Worker = worker.Surname;
            //ViewBag.Surname = worker.Surname;
            //ViewBag.Middlename = worker.Middlename;
            if (ModelState.IsValid)
            {
                _context.Add(education);
                await _context.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index), new {id = workerId });
            }
            ViewData["WorkerId"] = new SelectList(_context.Workers, "Id", "Middlename", education.WorkerId);
            
            return View(education);
        }



        public IActionResult AddEducation(int? Id)
        {
            if (Id == null || _context.Educations == null)
            {
                return NotFound();
            }

            var worker =  _context.Workers.Find(Id);
            ViewData["WorkerId"] = Id;

            //var education = _context.Educations.Find();
            ViewBag.WorkerId = Id;
            ViewBag.Name = worker.Name;
            ViewBag.Surname = worker.Surname;
            ViewBag.Middlename = worker.Middlename;
            ViewBag.Id = Id;

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddEducation([Bind("Id,diplomSeries,diplomNumber,special,yearEnd,WorkerId")] Education education)
        {
            if (ModelState.IsValid)
            {
                _context.Add(education);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["WorkerId"] = new SelectList(_context.Workers, "Id", "Middlename", education.WorkerId);
            return View(education);
        }

        // GET: Educations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            
            if (id == null || _context.Educations == null)
            {
                return NotFound();
            }

            var education = await _context.Educations.FindAsync(id);

            var workerId = education.WorkerId;
            Worker worker = await _context.Workers.FindAsync(workerId);
            ViewBag.WorkerId = workerId;
            ViewBag.Name = worker.Name;
            ViewBag.Surname = worker.Surname;
            ViewBag.Middlename = worker.Middlename;
            ViewBag.yearEnd = education.yearEnd;

            if (education == null)
            {
                return NotFound();
            }
           
            return View(education);
        }

        // POST: Educations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,diplomSeries,diplomNumber,special,yearEnd,WorkerId")] Education education)
        {
           
            var workerId = education.WorkerId;
           
            if (id != education.Id)
            {
                return NotFound();
            }
           
            if (ModelState.IsValid)
            {
                try
                {

                    _context.Update(education);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EducationExists(education.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                
                return RedirectToAction(nameof(Index), new {Id = workerId});
            }
            //ViewData["WorkerId"] = new SelectList(_context.Workers, "Id", "Middlename", education.WorkerId);
            return View(education);
        }

        // GET: Educations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Educations == null)
            {
                return NotFound();
            }

            var education = await _context.Educations.FindAsync(id);

            var workerId = education.WorkerId;
            Worker worker = await _context.Workers.FindAsync(workerId);
            ViewBag.WorkerId = workerId;
            ViewBag.Name = worker.Name;
            ViewBag.Surname = worker.Surname;
            ViewBag.Middlename = worker.Middlename;
            ViewBag.yearEnd = education.yearEnd;

            education = await _context.Educations
                .Include(e => e.Worker)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (education == null)
            {
                return NotFound();
            }

            return View(education);
        }

        // POST: Educations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Educations == null)
            {
                return Problem("Entity set 'AppDBContext.Educations'  is null.");
            }
            var education = await _context.Educations.FindAsync(id);
            int? workerId = education.WorkerId;
            if (education != null)
            {
                _context.Educations.Remove(education);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new {Id = workerId});
        }

        private bool EducationExists(int id)
        {
          return _context.Educations.Any(e => e.Id == id);
        }
    }
}
