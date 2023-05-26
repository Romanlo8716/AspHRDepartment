using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Laba1.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Laba1.Controllers
{
    public class PostsOfDepartmentsController : Controller
    {
        private readonly AppDBContext _context;

        public PostsOfDepartmentsController(AppDBContext context)
        {
            _context = context;
        }

        // GET: PostsOfDepartments
        public async Task<IActionResult> Index()
        {
            var appDBContext = _context.PostsOfDepartment.Include(p => p.Department).Include(p => p.Post);
            return View(await appDBContext.ToListAsync());
        }

        // GET: PostsOfDepartments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PostsOfDepartment == null)
            {
                return NotFound();
            }

            var postsOfDepartment = await _context.PostsOfDepartment
                .Include(p => p.Department)
                .Include(p => p.Post)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (postsOfDepartment == null)
            {
                return NotFound();
            }

            return View(postsOfDepartment);
        }

        // GET: PostsOfDepartments/Create
        public IActionResult Create()
        {
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name");
            ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Title");
            return View();
        }

        // POST: PostsOfDepartments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PostId,DepartmentId,Count")] PostsOfDepartment postsOfDepartment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(postsOfDepartment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name", postsOfDepartment.DepartmentId);
            ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Title", postsOfDepartment.PostId);
            return View(postsOfDepartment);
        }

        // GET: PostsOfDepartments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PostsOfDepartment == null)
            {
                return NotFound();
            }
            var idDep = _context.PostsOfDepartment.First(e => e.Id == id);
            var postCount = _context.DepartmentsAndPostsOfWorker.Where(e => e.DepartmentId == idDep.DepartmentId);

            ViewBag.PostsCount = postCount;
            var postsOfDepartment = await _context.PostsOfDepartment.FindAsync(id);
            if (postsOfDepartment == null)
            {
                return NotFound();
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name", postsOfDepartment.DepartmentId);
            ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Title", postsOfDepartment.PostId);
            return View(postsOfDepartment);
        }

        // POST: PostsOfDepartments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, int open, int close, [Bind("Id,PostId,DepartmentId,Count")] PostsOfDepartment postsOfDepartment)
        {
            if(open<close)
            {
                return RedirectToAction(nameof(ErrorCloseVacation), new { Id = id });
            }
            else
            {
                if (id != postsOfDepartment.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        postsOfDepartment.Count = open;
                        _context.Update(postsOfDepartment);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!PostsOfDepartmentExists(postsOfDepartment.Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return Redirect($"~/Departments/Details/{postsOfDepartment.DepartmentId}");
                }
                ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name", postsOfDepartment.DepartmentId);
                ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Title", postsOfDepartment.PostId);
                return View(postsOfDepartment);
            }
          
        }

        public IActionResult ErrorCloseVacation(int Id)
        {
            ViewBag.id = Id;
            return View();
        }

        // GET: PostsOfDepartments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PostsOfDepartment == null)
            {
                return NotFound();
            }

            var postsOfDepartment = await _context.PostsOfDepartment
                .Include(p => p.Department)
                .Include(p => p.Post)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (postsOfDepartment == null)
            {
                return NotFound();
            }

            return View(postsOfDepartment);
        }

        // POST: PostsOfDepartments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PostsOfDepartment == null)
            {
                return Problem("Entity set 'AppDBContext.PostsOfDepartment'  is null.");
            }
            var postsOfDepartment = await _context.PostsOfDepartment.FindAsync(id);
            var worker = _context.DepartmentsAndPostsOfWorker.Where(e => e.PostId == postsOfDepartment.PostId).Where(e => e.DepartmentId == postsOfDepartment.DepartmentId);
            if (postsOfDepartment != null)
            {
                _context.PostsOfDepartment.Remove(postsOfDepartment);
                _context.DepartmentsAndPostsOfWorker.RemoveRange(worker);
            }
            
            await _context.SaveChangesAsync();
            return Redirect($"~/Departments/Details/{postsOfDepartment.DepartmentId}");
        }

        private bool PostsOfDepartmentExists(int id)
        {
          return (_context.PostsOfDepartment?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
