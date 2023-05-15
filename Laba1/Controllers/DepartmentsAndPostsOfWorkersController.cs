using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Laba1.Models;
using Microsoft.AspNetCore.Http;

namespace Laba1.Controllers
{
    public class DepartmentsAndPostsOfWorkersController : Controller
    {
        private readonly AppDBContext _context;

        public DepartmentsAndPostsOfWorkersController(AppDBContext context)
        {
            _context = context;
        }

        // GET: DepartmentsAndPostsOfWorkers
        public async Task<IActionResult> Index()
        {
            var appDBContext = _context.DepartmentsAndPostsOfWorker.Include(d => d.Department).Include(d => d.Post).Include(d => d.Worker);
            return View(await appDBContext.ToListAsync());
        }

        // GET: DepartmentsAndPostsOfWorkers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DepartmentsAndPostsOfWorker == null)
            {
                return NotFound();
            }

            var departmentsAndPostsOfWorker = await _context.DepartmentsAndPostsOfWorker
                .Include(d => d.Department)
                .Include(d => d.Post)
                .Include(d => d.Worker)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (departmentsAndPostsOfWorker == null)
            {
                return NotFound();
            }

            return View(departmentsAndPostsOfWorker);
        }

        public IActionResult ErrorAddWorkerDesc(int id)
        {
            ViewBag.id = id;
            return View();
        }

        public IActionResult AddDepartment(int? id)
        {
            

            Worker worker = _context.Workers.Find(id);

            ViewBag.SurnameWorker = worker.Surname;
            ViewBag.NameWorker = worker.Name;
            ViewBag.MiddlenameWorker = worker.Middlename;
            ViewBag.idWorker = id;

            var publishingDBContext = _context.Departments.Include(b => b.AdressDepartment);

            return View(publishingDBContext.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddDepartment(int workerId, int departmentId)
        {
          return RedirectToAction(nameof(AddPost), new { idDep = departmentId, idWorker = workerId });
        }



        public async Task<IActionResult> AddPost(int? idDep, int? idWorker)
        {
            if (User.IsInRole("admin") || User.IsInRole("multiAdmin"))
            {


                Department department = await _context.Departments.FindAsync(idDep);
                Worker worker = await _context.Workers.FindAsync(idWorker);

                if (idDep != department.Id || idWorker != worker.Id)
                {
                    return NotFound();
                }

                ViewBag.NameDep = department.Name;
                @ViewBag.SurnameWorker = worker.Surname;
                @ViewBag.NameWorker = worker.Name;
                @ViewBag.MiddlenameWorker = worker.Middlename;
                ViewBag.idDep = idDep;
                ViewBag.idWorker = idWorker;
                return View(await _context.Posts.ToListAsync());
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddPost(int departmentId, int workerId, int postId)
        {


            return RedirectToAction(nameof(ConfirmSelection), new { idDep = departmentId, idWorker = workerId, idPost = postId });
        }

        public async Task<IActionResult> ConfirmSelection(int? idDep, int? idWorker, int? idPost)
        {
            if (User.IsInRole("admin") || User.IsInRole("multiAdmin"))
            {
                Department department = await _context.Departments.FindAsync(idDep);
                Worker worker = await _context.Workers.FindAsync(idWorker);
                Post post = await _context.Posts.FindAsync(idPost);
                AdressDepartment adressDepartment = await _context.AdressDepartments.FindAsync(department.idAdressDepartment);

                if (idDep != department.Id || idWorker != worker.Id || idPost != post.Id)
                {
                    return NotFound();
                }

                @ViewBag.NameWorker = worker.Name;
                @ViewBag.SurnameWorker = worker.Surname;
                @ViewBag.MiddlenameWorker = worker.Middlename;
                ViewBag.idWorker = idWorker;

                @ViewBag.NameDep = department.Name;
                @ViewBag.PhoneDep = department.Phone;
                @ViewBag.AdressDepCity = adressDepartment.City;
                @ViewBag.AdressDepStreet = adressDepartment.Street;
                @ViewBag.AdressDepHouse = adressDepartment.House;
                ViewBag.idDep = idDep;

                @ViewBag.TilePost = post.Title;
                @ViewBag.SalaryPost = post.Salary;
                ViewBag.idPost = idPost;
                return View();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmSelection(int departmentId, int workerId, int postId, [Bind("Id, WorkerId, DepartmentId, PostId")] DepartmentsAndPostsOfWorker departmentsAndPostsOfWorker)
        {
            
            var workerCount = _context.DepartmentsAndPostsOfWorker.Where(e => e.WorkerId == workerId).Where(e => e.DepartmentId == departmentId).Where(e => e.PostId == postId).ToList();

            if (workerCount.Count() == 0)
            {
                if (ModelState.IsValid)
                {
                    departmentsAndPostsOfWorker.DepartmentId = departmentId;
                    departmentsAndPostsOfWorker.WorkerId = workerId;
                    departmentsAndPostsOfWorker.PostId = postId;
                    _context.Add(departmentsAndPostsOfWorker);


                    await _context.SaveChangesAsync();
                    return Redirect($"~/Workers/IndexAllWorkers");

                }
                return View(departmentsAndPostsOfWorker);
            }
            else
            {
                return RedirectToAction(nameof(ErrorAddWorkerDesc), new { id = workerId });
            }
        

          
        }

        public void ErrorAddWorker()
        {

        }

        // GET: DepartmentsAndPostsOfWorkers/Create
        public IActionResult Create()
        {
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name");
            ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Title");
            ViewData["WorkerId"] = new SelectList(_context.Workers, "Id", "Middlename");
            return View();
        }

       

        // POST: DepartmentsAndPostsOfWorkers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,WorkerId,DepartmentId,PostId")] DepartmentsAndPostsOfWorker departmentsAndPostsOfWorker)
        {
            if (ModelState.IsValid)
            {
                _context.Add(departmentsAndPostsOfWorker);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name", departmentsAndPostsOfWorker.DepartmentId);
            ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Title", departmentsAndPostsOfWorker.PostId);
            ViewData["WorkerId"] = new SelectList(_context.Workers, "Id", "Middlename", departmentsAndPostsOfWorker.WorkerId);
            return View(departmentsAndPostsOfWorker);
        }

        // GET: DepartmentsAndPostsOfWorkers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (User.IsInRole("admin") || User.IsInRole("megaAdmin"))
            {


                if (id == null || _context.DepartmentsAndPostsOfWorker == null)
                {
                    return NotFound();
                }

                var departmentsAndPostsOfWorker = await _context.DepartmentsAndPostsOfWorker.FindAsync(id);

                int workerId = departmentsAndPostsOfWorker.WorkerId;

                Worker worker = await _context.Workers.FindAsync(workerId);

                ViewBag.NameWorker = worker.Name;
                ViewBag.SurnameWorker = worker.Surname;
                ViewBag.MiddlenameWorker = worker.Middlename;

                if (departmentsAndPostsOfWorker == null)
                {
                    return NotFound();
                }
                ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name", departmentsAndPostsOfWorker.DepartmentId);
                ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Title", departmentsAndPostsOfWorker.PostId);
                ViewData["WorkerId"] = new SelectList(_context.Workers, "Id", "Middlename", departmentsAndPostsOfWorker.WorkerId);
                return View(departmentsAndPostsOfWorker);
            }
            else
            {
                return NotFound();
            }
        }

        // POST: DepartmentsAndPostsOfWorkers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,WorkerId,DepartmentId,PostId")] DepartmentsAndPostsOfWorker departmentsAndPostsOfWorker)
        {
            if (id != departmentsAndPostsOfWorker.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(departmentsAndPostsOfWorker);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartmentsAndPostsOfWorkerExists(departmentsAndPostsOfWorker.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Redirect($"~/Workers/Intelligence/{departmentsAndPostsOfWorker.WorkerId}");
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name", departmentsAndPostsOfWorker.DepartmentId);
            ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Title", departmentsAndPostsOfWorker.PostId);
            ViewData["WorkerId"] = new SelectList(_context.Workers, "Id", "Middlename", departmentsAndPostsOfWorker.WorkerId);
            return View(departmentsAndPostsOfWorker);
        }

        // GET: DepartmentsAndPostsOfWorkers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DepartmentsAndPostsOfWorker == null)
            {
                return NotFound();
            }

            var departmentsAndPostsOfWorker = await _context.DepartmentsAndPostsOfWorker
                .Include(d => d.Department)
                .Include(d => d.Post)
                .Include(d => d.Worker)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (departmentsAndPostsOfWorker == null)
            {
                return NotFound();
            }

            return View(departmentsAndPostsOfWorker);
        }

        // POST: DepartmentsAndPostsOfWorkers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DepartmentsAndPostsOfWorker == null)
            {
                return Problem("Entity set 'AppDBContext.DepartmentsAndPostsOfWorker'  is null.");
            }
            var departmentsAndPostsOfWorker = await _context.DepartmentsAndPostsOfWorker.FindAsync(id);
            if (departmentsAndPostsOfWorker != null)
            {
                _context.DepartmentsAndPostsOfWorker.Remove(departmentsAndPostsOfWorker);
            }
            
            

            await _context.SaveChangesAsync();
            return Redirect($"~/Workers/Intelligence/{departmentsAndPostsOfWorker.WorkerId}");
        }

        private bool DepartmentsAndPostsOfWorkerExists(int id)
        {
          return (_context.DepartmentsAndPostsOfWorker?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
