using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Laba1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Laba1.Controllers
{
    public class AllWorkersVacationController : Controller
    {
        private readonly AppDBContext _context;
        public AllWorkersVacationController(AppDBContext context)
        {
            _context = context;
        }
        public async Task<ActionResult> IndexAsync()
        {
            var publishingDBContext = _context.Workers.Include(b => b.Department).Include(b => b.Post);

            return View(await publishingDBContext.ToListAsync());
        }

        // GET: AllWorkersVacationController/Details/5
        public ActionResult DetailsVacations()
        {
            return View();
        }

        // GET: AllWorkersVacationController/Create

        public IActionResult AddVacation()
        {
            //ViewData["WorkerId"] = new SelectList(_context.Workers, "Id", "Name");

            return View();

        }

        // POST: AllWorkersVacationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddVacation([Bind("Id,dateStart,dateEnd, typeVacation, WorkerId")] Vacation vacation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vacation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["idAdressDepartment"] = new SelectList(_context.AdressDepartments, "Id", "City", department.idAdressDepartment);

            return View(vacation);

        }

     
    }
}
