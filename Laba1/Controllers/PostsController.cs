using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Laba1.Models;
using Microsoft.Win32;

namespace Laba1.Controllers
{
    public class PostsController : Controller
    {
        private readonly AppDBContext _context;

        public PostsController(AppDBContext context)
        {
            _context = context;
        }

        // GET: Posts
        public async Task<IActionResult> Index()
        {
              return View(await _context.Posts.ToListAsync());
        }

     
        public async Task<IActionResult> Create()
        {

            if (User.IsInRole("multiAdmin"))
            {
                return View();
            }
            else
            {
                return NotFound();
            }
             
            
          
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,Title,Salary")] Post post)
        {
            if (ModelState.IsValid)
            {
                _context.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }

     

      

        // GET: Posts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (User.IsInRole("admin") || User.IsInRole("coach") || User.IsInRole("multiAdmin"))
            {
                if (id == null || _context.Posts == null)
                {
                    return NotFound();
                }

                var post = await _context.Posts
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (post == null)
                {
                    return NotFound();
                }

                return View(post);
            }
            else
            {
                return NotFound();
            }
        }

        // GET: Posts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (User.IsInRole("multiAdmin"))
            { 
                if (id == null || _context.Posts == null)
                {
                    return NotFound();
                }

            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
            }
            else
            {
                return NotFound();
            }
        
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Salary")] Post post)
        {
            if (id != post.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(post);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.Id))
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
            return View(post);
        }

        // GET: Posts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (User.IsInRole("multiAdmin"))
            {
                if (id == null || _context.Posts == null)
                {
                    return NotFound();
                }

                var post = await _context.Posts
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (post == null)
                {
                    return NotFound();
                }

                return View(post);
            }
            else
            {
                return NotFound();
            }
           
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Posts == null)
            {
                return Problem("Entity set 'AppDBContext.Posts'  is null.");
            }
            var post = await _context.Posts.FindAsync(id);
            if (post != null)
            {
                _context.Posts.Remove(post);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(int id)
        {
          return _context.Posts.Any(e => e.Id == id);
        }
    }
}
