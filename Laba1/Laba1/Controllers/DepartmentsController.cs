﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Laba1.Models;

namespace Laba1.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly AppDBContext _context;

        public DepartmentsController(AppDBContext context)
        {
            _context = context;
        }

        // GET: Departments
        public async Task<IActionResult> Index()
        {
            var publishingDBContext = _context.Departments.Include(b => b.AdressDepartment);

            return View(await publishingDBContext.ToListAsync());
        }

        
        public IActionResult AddWorker(int? id)
        {
            Department department =  _context.Departments.Find(id);

            ViewBag.NameDep = department.Name;

            ViewBag.idDep = id;

            var publishingDBContext = _context.Workers.Include(b => b.Department).Include(b => b.Post);

            return View( publishingDBContext.ToList());
          
        }

        [HttpPost]
        public async Task<IActionResult> AddWorker(int departmentId, int workerId)
        {
         

            return RedirectToAction(nameof(ChooseWorker), new { idDep = departmentId, idWorker = workerId});
        }

        public async Task<IActionResult> ChooseWorker(int? idDep, int? idWorker)
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

        [HttpPost]
        public async Task<IActionResult> ChooseWorker(int departmentId, int workerId, int postId)
        {


            return RedirectToAction(nameof(ConfirmSelectionAddWorker), new { idDep = departmentId, idWorker = workerId, idPost = postId });
        }

        public async Task<IActionResult> ConfirmSelectionAddWorker(int? idDep, int? idWorker, int? idPost)
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

        [HttpPost]
        public async Task<IActionResult> ConfirmSelectionAddWorker(int departmentId, int workerId, int postId)
        {
            Worker worker = await _context.Workers.FindAsync(workerId);
            worker.idDepartment = departmentId;
            worker.idPost = postId;
            _context.Update(worker);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Workers/Edit/5
        public async Task<IActionResult> EditWorker(int? id)
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
            ViewData["idPost"] = new SelectList(_context.Posts, "Id", "Title", worker.idPost);
            ViewData["idDepartment"] = new SelectList(_context.Departments, "Id", "Name", worker.idDepartment);
            return View(worker);
        }


      

        // POST: Workers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditWorker(int id, [Bind("Id,Name,Surname,Middlename,Phone,Gender,idPost,idDepartment")] Worker worker)
        {
            if (id != worker.Id)
            {
                return NotFound();
            }

            int? idDep = worker.idDepartment;

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
                return RedirectToAction(nameof(AddWorker), new {id = idDep});
            }
            ViewData["idPost"] = new SelectList(_context.Posts, "Id", "Title", worker.idPost);
            ViewData["idDepartment"] = new SelectList(_context.Departments, "Id", "Name", worker.idDepartment);
            return View(worker);
        }

        private bool WorkerExists(int id)
        {
            return _context.Workers.Any(e => e.Id == id);
        }

        // GET: Departments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Departments == null)
            {
                return NotFound();
            }

            var department = await _context.Departments.Include(b => b.AdressDepartment).FirstOrDefaultAsync(m => m.Id == id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // GET: Departments/Create
        public IActionResult Create()
        {

            ViewData["idAdressDepartment"] = new SelectList(_context.AdressDepartments, "Id", "City");
            
            return View();
        }

        // POST: Departments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Phone,idAdressDepartment")] Department department)
        {
            if (ModelState.IsValid)
            {
                _context.Add(department);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["idAdressDepartment"] = new SelectList(_context.AdressDepartments, "Id", "City", department.idAdressDepartment);
         
            return View(department);
        }

        // GET: Departments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
       
            if (id == null || _context.Departments == null)
            {
                return NotFound();
            }

            var department = await _context.Departments.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }
            ViewData["idAdressDepartment"] = new SelectList(_context.AdressDepartments, "Id", "City", department.idAdressDepartment);
            return View(department);
        }

        // POST: Departments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Phone,idAdressDepartment")] Department department)
        {
           
            if (id != department.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(department);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartmentExists(department.Id))   
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
            ViewData["idAdressDepartment"] = new SelectList(_context.AdressDepartments, "Id", "City", department.idAdressDepartment);
            return View(department);
        }

       

        // GET: Departments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Departments == null)
            {
                return NotFound();
            }

            var department = await _context.Departments.Include(b => b.AdressDepartment).FirstOrDefaultAsync(m => m.Id == id);

            if (department == null)
            {
                return NotFound();
            }
            
            return View(department);
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Departments == null)
            {
                return Problem("Entity set 'AppDBContext.Departments'  is null.");
            }
            var department = await _context.Departments.FindAsync(id);
            if (department != null)
            {
                _context.Departments.Remove(department);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DepartmentExists(int id)
        {
          return _context.Departments.Any(e => e.Id == id);
        }

      
    }
}
