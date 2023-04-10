using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Laba1.Models;

namespace Laba1.Controllers
{
    public class AdressDepartmentsController : Controller
    {
        private readonly AppDBContext _context;

        public AdressDepartmentsController(AppDBContext context)
        {
            _context = context;
        }

        // GET: AdressDepartments
        public async Task<IActionResult> Index()
        {
              return View(await _context.AdressDepartments.ToListAsync());
        }

        // GET: AdressDepartments/Details/5
        public async Task<IActionResult> Details(int? id)
        {

            if (User.IsInRole("admin"))
            {
                if (id == null || _context.AdressDepartments == null)
                {
                    return NotFound();
                }

                var adressDepartment = await _context.AdressDepartments
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (adressDepartment == null)
                {
                    return NotFound();
                }

                return View(adressDepartment);
            }
            else
            {
                return NotFound();
            }
        }

        // GET: AdressDepartments/Create
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

        // POST: AdressDepartments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,City,Street,House")] AdressDepartment adressDepartment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(adressDepartment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(adressDepartment);
        }

        // GET: AdressDepartments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (User.IsInRole("admin"))
            {
                if (id == null || _context.AdressDepartments == null)
                {
                    return NotFound();
                }

                var adressDepartment = await _context.AdressDepartments.FindAsync(id);
                if (adressDepartment == null)
                {
                    return NotFound();
                }
                return View(adressDepartment);
            }
            else
            {
                return NotFound();
            }
        }

        // POST: AdressDepartments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,City,Street,House")] AdressDepartment adressDepartment)
        {
            if(User.IsInRole("admin"))
                {
                if (id != adressDepartment.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(adressDepartment);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!AdressDepartmentExists(adressDepartment.Id))
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
                return View(adressDepartment);
            }
            else {
                return NotFound();
            }
        }

        // GET: AdressDepartments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (User.IsInRole("admin"))
            {
                if (User.IsInRole("admin"))
                {
                    if (id == null || _context.AdressDepartments == null)
                    {
                        return NotFound();
                    }

                    var adressDepartment = await _context.AdressDepartments
                        .FirstOrDefaultAsync(m => m.Id == id);
                    if (adressDepartment == null)
                    {
                        return NotFound();
                    }

                    return View(adressDepartment);
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return NotFound();
            }
        }

        // POST: AdressDepartments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AdressDepartments == null)
            {
                return Problem("Entity set 'AppDBContext.AdressDepartments'  is null.");
            }
            var adressDepartment = await _context.AdressDepartments.FindAsync(id);
            if (adressDepartment != null)
            {
                _context.AdressDepartments.Remove(adressDepartment);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdressDepartmentExists(int id)
        {
          return _context.AdressDepartments.Any(e => e.Id == id);
        }
    }
}
