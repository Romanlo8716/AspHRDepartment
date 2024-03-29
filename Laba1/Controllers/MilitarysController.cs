﻿using Laba1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Laba1.Controllers
{
    public class MilitarysController : Controller
    {

        private readonly AppDBContext _context;

        public MilitarysController(AppDBContext context)
        {
            _context = context;
        }

        // GET: MilitarysController
        public async Task<IActionResult> Index(int? Id)
        {
            if (!User.IsInRole("guest"))
            {
                if (Id == null || _context.Workers == null)
                {
                    return NotFound();
                }

                var worker = await _context.Workers.FirstOrDefaultAsync(m => m.Id == Id);
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

        public async Task<IActionResult> AddMilitary(int? id)
        {
            if (User.IsInRole("admin") || User.IsInRole("multiAdmin"))
            {
                var worker = await _context.Workers.FindAsync(id);

                ViewBag.id = id;
                ViewBag.Name = worker.Name;
                ViewBag.Surname = worker.Surname;
                ViewBag.Middlename = worker.Middlename;
                @ViewBag.Phone = worker.Phone;
                @ViewBag.Gender = worker.Gender;
               


                if (id == null || _context.Workers == null)
                {
                    return NotFound();
                }


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


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMilitary(int id, [Bind("Id,Name,Surname,Middlename,Phone,DateOfBirth, CityHabitation, StreetHabitation, HouseHabitation, FamilyStatus, Children, Email, SeriesPass, NumberPass, IssuedByWhom, DateOfIssue, DivisionCode, NumberSnils, NumberInn, Gender, military_title, shelf_life, stock_category, profile, vus, name_kommis, Photo, DescriptionWorker,  dismissStatus")] Worker worker)
        {
            ViewBag.id = id;
            ViewBag.Name = worker.Name;
            ViewBag.Surname = worker.Surname;
            ViewBag.Middlename = worker.Middlename;
            @ViewBag.Phone = worker.Phone;
            @ViewBag.Gender = worker.Gender;
      

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
                return Redirect($"~/Workers/Intelligence/{id}");
            }

            return View(worker);
        }

      

        // GET: MilitarysController/Delete/5
        public ActionResult Delete(int id)
        {
            if (User.IsInRole("admin") || User.IsInRole("multiAdmin"))
            {


                return View();
            }
            else
            {
                return NotFound();
            }
        }

        // POST: MilitarysController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private bool WorkerExists(int id)
        {
            return _context.Workers.Any(e => e.Id == id);
        }
    }
}
