using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Laba1.Models;
using Laba1.Models.ViewModels;
using System.Numerics;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Net;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace Laba1.Controllers
{
    public class WorkersController : Controller
    {
        private readonly AppDBContext _context;
        private readonly IWebHostEnvironment _appEnvironment;

        public WorkersController(AppDBContext context, IWebHostEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        // GET: Workers
        public async Task<IActionResult> Index()
        {
            var worker =  _context.DepartmentsAndPostsOfWorker.Include(e => e.Worker).Include(e => e.Post).Include(e => e.Department).OrderBy(o => o.DepartmentId);

            var post = _context.Posts;

            ViewBag.Posts = post;

            ViewBag.Photodata = _appEnvironment.WebRootPath;

            return View(await worker.ToListAsync());
           
        }


        public async Task<IActionResult> IndexAllWorkers()
        {
            if (User.IsInRole("admin") || User.IsInRole("multiAdmin"))
            {


                ViewBag.depAndPost = _context.DepartmentsAndPostsOfWorker.Include(e => e.Worker);
                var worker = _context.Workers;
                ViewBag.allWorkers = worker.Count();
                return View(await worker.ToListAsync());
            }
            else
            {
                return NotFound();
            }
        }

        public async Task<IActionResult> GetCandidates()
        {


            var work = _context.Workers;

            ViewBag.Work = work;



           

            var worker = _context.Workers;

            var depAndPost = _context.DepartmentsAndPostsOfWorker.Include(e => e.Worker);

            List<Worker> newListWorker = new List<Worker>();

            bool flag = false;

            foreach(var item1 in worker.ToList())
            {
               
                foreach(var item2 in depAndPost.ToList())
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
                    newListWorker.Add(item1);
                }
                flag = false;
            }

            
            ViewBag.Worker = newListWorker.ToList();
            ViewBag.allWorkers = newListWorker.Count();
            return View(newListWorker);

        }

        public async Task<IActionResult> GetWorkersOfCompany()
        {
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

            ViewBag.Workers = newListWorker.ToArray(); 
          
            ViewBag.allWorkers = newListWorker.Count();

            ViewBag.Department = depAndPost.ToArray();

            return View();

        }

        public async Task<IActionResult> GetDismissWorkers()
        {

            var worker = _context.Workers.Where(e => e.dissmisStatus == true);
            ViewBag.allWorkers = worker.Count();
            return View(await worker.ToListAsync());

        }



        // GET: Workers/Details/5
        public async Task<IActionResult> Details(int? id)
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

        public async Task<IActionResult> Intelligence(int? id)
        {
            if (User.IsInRole("coach") || User.IsInRole("admin") || User.IsInRole("multiAdmin"))
            {


                ViewBag.workerId = id;
                var worker = await _context.Workers.FirstOrDefaultAsync(m => m.Id == id);

                if (id == null || _context.Workers == null)
                {
                    return NotFound();
                }
                if (!worker.Photo.IsNullOrEmpty())
                {
                    byte[] photodata = System.IO.File.ReadAllBytes(_appEnvironment.WebRootPath + worker.Photo);
                    ViewBag.Photodata = photodata;
                }
                else
                {
                    ViewBag.Photodata = null;
                }


                DescriptionsWorker descWorker = new DescriptionsWorker();
                descWorker.worker = worker;
                descWorker.laborBooks = _context.LaborBook.Include(e => e.Worker).Where(e => id == e.WorkerId);
                descWorker.educations = _context.Educations.Include(e => e.Worker).Where(e => id == e.WorkerId);
                descWorker.vacations = _context.Vacations.Include(e => e.Worker).Where(e => id == e.WorkerId);
                descWorker.medicalBooks = _context.MedicalBook.Include(e => e.Worker).Where(e => id == e.WorkerId);
                //descWorker.departmentsOfWorkers = _context.DepartmentsOfWorker.Include(e => e.Worker).Include(e => e.Department).Where(e => id == e.WorkerId);
                //descWorker.postsOfWorkers = _context.PostsOfWorker.Include(e => e.Worker).Include(e => e.Post).Where(e => id == e.WorkerId);

                descWorker.departmentsAndPostsOfWorkers = _context.DepartmentsAndPostsOfWorker.Include(e => e.Worker).Include(e => e.Department).Include(e => e.Post).Where(e => id == e.WorkerId);

                if (worker == null)
                {
                    return NotFound();
                }

                ViewData["Department"] = new SelectList(_context.Departments, "Id", "Name", worker.Id);

                return View(descWorker);
            }
            else
            {
                return NotFound();
            }
        }


     
       

        // GET: Workers/Create
        public IActionResult Create()
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

        // POST: Workers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Surname,Middlename,Phone,DateOfBirth, CityHabitation, StreetHabitation, HouseHabitation, FamilyStatus, Children, Email, SeriesPass, NumberPass, IssuedByWhom, DateOfIssue, DivisionCode, NumberSnils, NumberInn, Gender, military_title, shelf_life, stock_category, profile, vus, name_kommis, Photo, DescriptionWorker,  dismissStatus")] Worker worker, IFormFile upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null)
                {
                    string path = "/Files/" + upload.FileName;
                    using (var fileStream = new
                   FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                    {
                        await upload.CopyToAsync(fileStream);
                    }
                    worker.Photo = path;
                }
                _context.Add(worker);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                string errorMessages = "";
                // проходим по всем элементам в ModelState
                foreach (var item in ModelState)
                {
                    // если для определенного элемента имеются ошибки
                    if (item.Value.ValidationState == ModelValidationState.Invalid)
                    {
                        errorMessages = $"{errorMessages}\nОшибки для свойства {item.Key}:\n";
                        // пробегаемся по всем ошибкам
                        foreach (var error in item.Value.Errors)
                        {
                            errorMessages = $"{errorMessages}{error.ErrorMessage}\n";
                        }
                    }
                }
                return RedirectToAction(nameof(error), new { message = errorMessages });

            }
          
            

         
        }

        public string error(string message)
        {
            return message;
        }

        // GET: Workers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (User.IsInRole("admin") || User.IsInRole("multiAdmin"))
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

                if (!worker.Photo.IsNullOrEmpty())
                {
                    byte[] photodata = System.IO.File.ReadAllBytes(_appEnvironment.WebRootPath + worker.Photo);
                    ViewBag.Photodata = photodata;
                }
                else
                {
                    ViewBag.Photodata = null;
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Surname,Middlename,Phone,DateOfBirth, CityHabitation, StreetHabitation, HouseHabitation, FamilyStatus, Children, Email, SeriesPass, NumberPass, IssuedByWhom, DateOfIssue, DivisionCode, NumberSnils, NumberInn, Gender, military_title, shelf_life, stock_category, profile, vus, name_kommis, Photo, DescriptionWorker,  dismissStatus")] Worker worker, IFormFile? upload)
        {
            if (id != worker.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (upload != null)
                {
                    string path = "/Files/" + upload.FileName;
                    using (var fileStream = new
                   FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                    {
                        await upload.CopyToAsync(fileStream);
                    }
                    //if (!worker.Photo.IsNullOrEmpty())
                    //{
                    //    System.IO.File.Delete(_appEnvironment.WebRootPath + worker.Photo);
                    //}
                    worker.Photo = path;
                }

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
                return RedirectToAction(nameof(Intelligence), new { id = id });
            }
           
            return View(worker);
        }

        public async Task<IActionResult> Dismiss(int? id)
        {
            if (User.IsInRole("admin") || User.IsInRole("multiAdmin"))
            {
                if (id == null || _context.Workers == null)
                {
                    return NotFound();
                }


                var worker = await _context.Workers.FirstOrDefaultAsync(m => m.Id == id);

                if (!worker.Photo.IsNullOrEmpty())
                {
                    byte[] photodata = System.IO.File.ReadAllBytes(_appEnvironment.WebRootPath + worker.Photo);
                    ViewBag.Photodata = photodata;
                }
                else
                {
                    ViewBag.Photodata = null;
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
        public async Task<IActionResult> Dismiss(int id, [Bind("Id,Name,Surname,Middlename,Phone,DateOfBirth, CityHabitation, StreetHabitation, HouseHabitation, FamilyStatus, Children, Email, SeriesPass, NumberPass, IssuedByWhom, DateOfIssue, DivisionCode, NumberSnils, NumberInn, Gender, military_title, shelf_life, stock_category, profile, vus, name_kommis, Photo, DescriptionWorker,  dismissStatus")] Worker worker, IFormFile? upload)
        {
            if (id != worker.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                worker.dissmisStatus = true;
                if (upload != null)
                {
                    string path = "/Files/" + upload.FileName;
                    using (var fileStream = new
                   FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                    {
                        await upload.CopyToAsync(fileStream);
                    }
                    //if (!worker.Photo.IsNullOrEmpty())
                    //{
                    //    System.IO.File.Delete(_appEnvironment.WebRootPath + worker.Photo);
                    //}
                    worker.Photo = path;
                }
                var workerinDep = _context.DepartmentsAndPostsOfWorker.Where(x => x.WorkerId == id);

                if (workerinDep != null)
                {
                    _context.DepartmentsAndPostsOfWorker.RemoveRange(workerinDep);
                }
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
                return RedirectToAction(nameof(IndexAllWorkers));
            }

            return View(worker);
        }

        public async Task<IActionResult> Recover(int? id)
        {
            if (User.IsInRole("admin") || User.IsInRole("multiAdmin"))
            {
                if (id == null || _context.Workers == null)
                {
                    return NotFound();
                }


                var worker = await _context.Workers.FirstOrDefaultAsync(m => m.Id == id);

                if (!worker.Photo.IsNullOrEmpty())
                {
                    byte[] photodata = System.IO.File.ReadAllBytes(_appEnvironment.WebRootPath + worker.Photo);
                    ViewBag.Photodata = photodata;
                }
                else
                {
                    ViewBag.Photodata = null;
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
        public async Task<IActionResult> Recover(int id, [Bind("Id,Name,Surname,Middlename,Phone,DateOfBirth, CityHabitation, StreetHabitation, HouseHabitation, FamilyStatus, Children, Email, SeriesPass, NumberPass, IssuedByWhom, DateOfIssue, DivisionCode, NumberSnils, NumberInn, Gender, military_title, shelf_life, stock_category, profile, vus, name_kommis, Photo, DescriptionWorker,  dismissStatus")] Worker worker, IFormFile? upload)
        {
            if (id != worker.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                worker.dissmisStatus = false;
                if (upload != null)
                {
                    string path = "/Files/" + upload.FileName;
                    using (var fileStream = new
                   FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                    {
                        await upload.CopyToAsync(fileStream);
                    }
                    //if (!worker.Photo.IsNullOrEmpty())
                    //{
                    //    System.IO.File.Delete(_appEnvironment.WebRootPath + worker.Photo);
                    //}
                    worker.Photo = path;
                }
            
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
                return RedirectToAction(nameof(IndexAllWorkers));
            }

            return View(worker);
        }

        // GET: Workers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (User.IsInRole("multiAdmin"))
            {
                if (id == null || _context.Workers == null)
                {
                    return NotFound();
                }


                var worker = await _context.Workers.FirstOrDefaultAsync(m => m.Id == id);

                if (!worker.Photo.IsNullOrEmpty())
                {
                    byte[] photodata = System.IO.File.ReadAllBytes(_appEnvironment.WebRootPath + worker.Photo);
                    ViewBag.Photodata = photodata;
                }
                else
                {
                    ViewBag.Photodata = null;
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

        // POST: Workers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Workers == null)
            {
                return Problem("Entity set 'AppDBContext.Workers'  is null.");
            }
            var worker =  _context.Workers.Find(id);
            var workerinDep = _context.DepartmentsAndPostsOfWorker.Where(x => x.WorkerId == id);
            var workerinEduc = _context.Educations.Where(x => x.WorkerId == id);
            var workerinMed = _context.MedicalBook.Where(x => x.WorkerId == id);
            var workerinLabor = _context.LaborBook.Where(x => x.WorkerId == id);
            var workerinVacation= _context.Vacations.Where(x => x.WorkerId == id);



            if (workerinDep != null)
            {
                _context.DepartmentsAndPostsOfWorker.RemoveRange(workerinDep);
            }

            if (workerinEduc != null)
            {
                _context.Educations.RemoveRange(workerinEduc);
            }
        

            if (workerinMed != null)
            {
                _context.MedicalBook.RemoveRange(workerinMed);
            }

            if (workerinLabor != null)
            {
                _context.LaborBook.RemoveRange(workerinLabor);
            }

            if (workerinVacation != null)
            {
                _context.Vacations.RemoveRange(workerinVacation);
            }

           if(worker != null)
            {
                _context.Workers.Remove(worker);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(IndexAllWorkers));
        }

        public IActionResult ReportOfVacation()
        {
           

                return View();
         

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public FileResult GetReportVacation(DateTime dateStartPost, DateTime dateEndPost)
        {


            // Путь к файлу с шаблоном
            string path = "/Reports/reportVacations_template.xlsx";
            //Путь к файлу с результатом
            string result = "/Reports/reportVacations.xlsx";

            FileInfo fi = new FileInfo(_appEnvironment.WebRootPath + path);
            FileInfo fr = new FileInfo(_appEnvironment.WebRootPath + result);

            //будем использовть библитотеку не для коммерческого использования
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage excelPackage = new ExcelPackage(fi))
            {
                //устанавливаем поля документа
                excelPackage.Workbook.Properties.Author = "Комаров Р.Д.";
                excelPackage.Workbook.Properties.Title = "Список отпусков сотрудников";
                excelPackage.Workbook.Properties.Subject = "Отпуска сотрудников";
                excelPackage.Workbook.Properties.Created = DateTime.Now;
                //плучаем лист по имени.
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets["Vacations"];

                int startLine = 3;
                List<Vacation> Vacations = _context.Vacations.Include(e => e.Worker).Where(e => e.dateStart >= dateStartPost).Where(e => e.dateStart <= dateEndPost).ToList();

              

                foreach (var vacation in Vacations)
                {


                    string fio = $"{vacation.Worker.Surname} {vacation.Worker.Name} {vacation.Worker.Middlename}";
                    string dateStart = vacation.dateStart.ToString("d");
                    string dateEnd = vacation.dateEnd.ToString("d");

                    worksheet.Cells[startLine, 1].Value = startLine - 2;
                    worksheet.Cells[startLine, 2].Value = fio;
                    worksheet.Cells[startLine, 3].Value = dateStart;
                    worksheet.Cells[startLine, 4].Value = dateEnd;
                    worksheet.Cells[startLine, 5].Value = vacation.typeVacation;
                    startLine++;
                }

                worksheet.Cells[$"A3:E{startLine}"].Sort(x => x.SortBy.Column(1).ThenSortBy.Column(2));

                //созраняем в новое место
                excelPackage.SaveAs(fr);
                // Тип файла - content-type
                string file_type = "application/vnd.openxmlformatsofficedocument.spreadsheetml.sheet";
                // Имя файла - необязательно
                string file_name = "reportVacations.xlsx";
                return File(result, file_type, file_name);

            }

        }

        public FileResult GetReportVacationAll()
        {


            // Путь к файлу с шаблоном
            string path = "/Reports/reportVacations_template.xlsx";
            //Путь к файлу с результатом
            string result = "/Reports/reportVacations.xlsx";

            FileInfo fi = new FileInfo(_appEnvironment.WebRootPath + path);
            FileInfo fr = new FileInfo(_appEnvironment.WebRootPath + result);

            //будем использовть библитотеку не для коммерческого использования
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage excelPackage = new ExcelPackage(fi))
            {
                //устанавливаем поля документа
                excelPackage.Workbook.Properties.Author = "Комаров Р.Д.";
                excelPackage.Workbook.Properties.Title = "Список отпусков сотрудников";
                excelPackage.Workbook.Properties.Subject = "Отпуска сотрудников";
                excelPackage.Workbook.Properties.Created = DateTime.Now;
                //плучаем лист по имени.
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets["Vacations"];

                int startLine = 3;
                List<Vacation> Vacations = _context.Vacations.Include(e => e.Worker).ToList();



                foreach (var vacation in Vacations)
                {


                    string fio = $"{vacation.Worker.Surname} {vacation.Worker.Name} {vacation.Worker.Middlename}";
                    string dateStart = vacation.dateStart.ToString("d");
                    string dateEnd = vacation.dateEnd.ToString("d");

                    worksheet.Cells[startLine, 1].Value = startLine - 2;
                    worksheet.Cells[startLine, 2].Value = fio;
                    worksheet.Cells[startLine, 3].Value = dateStart;
                    worksheet.Cells[startLine, 4].Value = dateEnd;
                    worksheet.Cells[startLine, 5].Value = vacation.typeVacation;
                    startLine++;
                }

                worksheet.Cells[$"A3:E{startLine}"].Sort(x => x.SortBy.Column(1).ThenSortBy.Column(2));

                //созраняем в новое место
                excelPackage.SaveAs(fr);
                // Тип файла - content-type
                string file_type = "application/vnd.openxmlformatsofficedocument.spreadsheetml.sheet";
                // Имя файла - необязательно
                string file_name = "reportVacations.xlsx";
                return File(result, file_type, file_name);

            }

        }

        public FileResult GetReportWorkers()
        {


            // Путь к файлу с шаблоном
            string path = "/Reports/reportWorkers_template.xlsx";
            //Путь к файлу с результатом
            string result = "/Reports/reportWorkers.xlsx";

            FileInfo fi = new FileInfo(_appEnvironment.WebRootPath + path);
            FileInfo fr = new FileInfo(_appEnvironment.WebRootPath + result);

            //будем использовть библитотеку не для коммерческого использования
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage excelPackage = new ExcelPackage(fi))
            {
                //устанавливаем поля документа
                excelPackage.Workbook.Properties.Author = "Комаров Р.Д.";
                excelPackage.Workbook.Properties.Title = "Список сотрудников";
                excelPackage.Workbook.Properties.Subject = "Сотрудники";
                excelPackage.Workbook.Properties.Created = DateTime.Now;
                //плучаем лист по имени.
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets["Workers"];

                int startLine = 3;
                List<DepartmentsAndPostsOfWorker> Workers = _context.DepartmentsAndPostsOfWorker.Include(e => e.Worker).Include(e => e.Department).Include(e => e.Post).ToList();



                foreach (var worker in Workers)
                {


                    string fio = $"{worker.Worker.Surname} {worker.Worker.Name} {worker.Worker.Middlename}";
                   

                    worksheet.Cells[startLine, 1].Value = startLine - 2;
                    worksheet.Cells[startLine, 2].Value = fio;
                    worksheet.Cells[startLine, 3].Value = worker.Department.Name;
                    worksheet.Cells[startLine, 4].Value = worker.Post.Title;
                  
                    startLine++;
                }

                worksheet.Cells[$"A3:E{startLine}"].Sort(x => x.SortBy.Column(1));

                //созраняем в новое место
                excelPackage.SaveAs(fr);
                // Тип файла - content-type
                string file_type = "application/vnd.openxmlformatsofficedocument.spreadsheetml.sheet";
                // Имя файла - необязательно
                string file_name = "reportVacations.xlsx";
                return File(result, file_type, file_name);

            }

        }

        private bool WorkerExists(int id)
        {
          return _context.Workers.Any(e => e.Id == id);
        }
    }
}
