using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Laba1.Models;
using Laba1.Models.ViewModels;

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
            var departments = _context.Departments.Include(b => b.AdressDepartment);

            var numberWorkers = _context.DepartmentsAndPostsOfWorker.ToArray();

            ViewBag.NumberWorkers = numberWorkers;

            var publishingDBContext = _context.Departments.Include(b => b.AdressDepartment);

            return View(await publishingDBContext.ToListAsync());
        }

        
        public IActionResult AddWorker(int? id)
        {
            if (User.IsInRole("admin"))
            {
                Department department = _context.Departments.Find(id);

                ViewBag.NameDep = department.Name;

                ViewBag.idDep = id;

                var publishingDBContext = _context.Workers;

                var workers = _context.Workers;

                var depAndPost = _context.DepartmentsAndPostsOfWorker.Include(e => e.Worker).Include(e => e.Department).Include(e => e.Post);

                List<Worker> newListWorker = new List<Worker>();

                bool flag = false;

                foreach (var item1 in workers.ToList())
                {

                    foreach (var item2 in depAndPost.ToList())
                    {
                        if (item1.Id == item2.WorkerId)
                        {
                            flag = true;
                            break;
                        }
                        else
                        {
                            flag = false;

                        }


                    }
                    if (flag == true && item1.dissmisStatus == false)
                    {
                        newListWorker.Add(item1);
                    }
                    flag = false;
                }

                ViewBag.WorkersOfCompany = newListWorker.ToArray();
                ViewBag.Department = depAndPost.ToArray();


                List<Worker> newListCandidates = new List<Worker>();

                flag = false;

                foreach (var item1 in workers.ToList())
                {

                    foreach (var item2 in depAndPost.ToList())
                    {
                        if (item1.Id == item2.WorkerId)
                        {
                            flag = true;
                            break;
                        }
                        else
                        {
                            flag = false;
                        }


                    }
                    if (flag == false && item1.dissmisStatus == false)
                    {
                        newListCandidates.Add(item1);
                    }
                    flag = false;
                }


                ViewBag.Candidates = newListCandidates.ToArray();

                return View(publishingDBContext.ToList());
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddWorker(int departmentId, int workerId)
        {
            return RedirectToAction(nameof(ChooseWorker), new { idDep = departmentId, idWorker = workerId});
        }

        public async Task<IActionResult> ChooseWorker(int? idDep, int? idWorker)
        {
            if (User.IsInRole("admin"))
            {


                Department department = await _context.Departments.FindAsync(idDep);
                Worker worker = await _context.Workers.FindAsync(idWorker);

                if (idDep != department.Id || idWorker != worker.Id)
                {
                    return NotFound();
                }

                ViewBag.NameDep = department.Name;
                ViewBag.SurnameWorker = worker.Surname;
                ViewBag.NameWorker = worker.Name;
                ViewBag.MiddlenameWorker = worker.Middlename;
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
        public async Task<IActionResult> ChooseWorker(int departmentId, int workerId, int postId)
        {


            return RedirectToAction(nameof(ConfirmSelectionAddWorker), new { idDep = departmentId, idWorker = workerId, idPost = postId });
        }

        public async Task<IActionResult> ConfirmSelectionAddWorker(int? idDep, int? idWorker, int? idPost)
        {
            if(User.IsInRole("admin"))
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
            else { 
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmSelectionAddWorker(int departmentId, int workerId, int postId, [Bind("Id, WorkerId, DepartmentId, PostId")] DepartmentsAndPostsOfWorker departmentsAndPostsOfWorker)
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
                    return RedirectToAction(nameof(Index));
                }
                return View(departmentsAndPostsOfWorker);
            }
            else
            {
                return RedirectToAction(nameof(ErrorAddWorker), new { id = departmentId });
            }
        }

        public IActionResult ErrorAddWorker(int id)
        {
            ViewBag.id = id;
            return View();
        }

        // GET: Workers/Edit/5
        public async Task<IActionResult> EditWorker(int? id)
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
        public async Task<IActionResult> EditWorker(int id, [Bind("Id,Name,Surname,Middlename,Phone,Gender")] Worker worker)
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
                return RedirectToAction(nameof(AddWorker), new {id = 1});
            }
          
            return View(worker);
        }

        private bool WorkerExists(int id)
        {
            return _context.Workers.Any(e => e.Id == id);
        }

        // GET: Departments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (User.IsInRole("admin") || User.IsInRole("megaAdmin") || User.IsInRole("coach") ||User.IsInRole("multiAdmin"))
            {
                if (id == null || _context.Departments == null)
                {
                    return NotFound();
                }

                DescriptionDepartment descriptionDepartment = new DescriptionDepartment();

                var department = await _context.Departments.Include(b => b.AdressDepartment).FirstOrDefaultAsync(m => m.Id == id);

                //var WorkersInDepartment = await _context.DepartmentsAndPostsOfWorker.Include(e => e.Worker).Include(e => e.Department).Include(e => e.Post).FirstOrDefaultAsync(f => f.DepartmentId == id);

                //ViewBag.WorkersInDep = _context.DepartmentsAndPostsOfWorker.Where(f => f.DepartmentId == id).ToList();

                //ViewBag.WorkersSurname = WorkersInDepartment.Worker.Surname;

                descriptionDepartment.department = department; 
                descriptionDepartment.departmentsAndPostsOfWorkers = _context.DepartmentsAndPostsOfWorker.Include(e => e.Worker).Include(e => e.Department).Include(e => e.Post).Where(b => b.DepartmentId == id).ToArray();


                ViewBag.City = department.AdressDepartment.City;
                ViewBag.Street = department.AdressDepartment.Street;
                ViewBag.House = department.AdressDepartment.House;
                ViewBag.DepId = id;
                ViewBag.NameDepartment = department.Name;

                if (department == null)
                {
                    return NotFound();
                }

                return View(descriptionDepartment);
            }
            else
            {
                return NotFound();
            }
        }

        // GET: Departments/Create
        public IActionResult Create()
        {
            if (User.IsInRole("admin"))
            {
                
                //ViewData["idAdressDepartment"] = new SelectList(_context.AdressDepartments, "Id", "City");
                //ViewData["streetAdress"] = new SelectList(_context.AdressDepartments, "Id", "Street");

               

               

                ViewData["idAdressDepartment"] = new SelectList(_context.AdressDepartments, "Id", "FullAddress");

                return View();
            }
            else
            {
                return NotFound();
            }
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
            if (User.IsInRole("multiAdmin"))
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
            else
            {
                return NotFound();
            }
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
            if (User.IsInRole("multiAdmin"))
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
            else
            {
                return NotFound();
            }
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
