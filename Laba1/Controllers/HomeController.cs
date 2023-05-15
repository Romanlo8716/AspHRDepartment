using Laba1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;



namespace Laba1.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDBContext _context;

      

   

        public HomeController(AppDBContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var departments = _context.Departments.Include(b => b.AdressDepartment);

            var posts = _context.Posts;

            var numberWorkers = _context.DepartmentsAndPostsOfWorker;



            ViewBag.DepartmentCount = departments.Count();

            ViewBag.PostCount = posts.Count();

           ViewBag.WorkerCount = numberWorkers.Count();




            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}