using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using VenketCorePracticeCore.Models;
using VenketCorePracticeCore.Models.ViewModels;

namespace VenketCorePracticeCore.Controllers
{
    public class HomeController:Controller
    {
        private readonly IEmployeeRepository _repository;
        private readonly IHostingEnvironment _hostingEnvironment;

        public HomeController(IEmployeeRepository repository, IHostingEnvironment hostingEnvironment)
        {
            _repository = repository;
            this._hostingEnvironment = hostingEnvironment;
        }

        //public string Index()
        //{
        //    return "Hello Tayyab message Come fromm Home Controller";
        //}

        public JsonResult Index()
        {
            return Json(_repository.GetEmployeeById(2));
        }

        public IActionResult Detail(int id)
        {
            var emp = _repository.GetEmployeeById(id);
            if(emp==null)
            {
                //Response.StatusCode = 404;
                //return View("EmployeeNotFound", id);
                return NotFound();
            }
            ViewData["title"] = "Emp-Details";
          //  Employee employee = _repository.GetEmployeeById(id);
            return View(emp);
        }

        //[ActionName("List")]
        public IActionResult ShowAllEmployee()
        {
            var listEmp = _repository.GetAllEmployeesList();

            return View(listEmp);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeesViewModel obj)
        {

            if(ModelState.IsValid)
            {
                string uniqueFile = null;


                //if(obj.imagePath!=null)  for Single image
                //{
                //    string UploadsFolders = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                //    uniqueFile = Guid.NewGuid().ToString() +"_"+ obj.imagePath.FileName;
                // string FilePath=Path.Combine(UploadsFolders, uniqueFile);
                //    obj.imagePath.CopyTo(new FileStream(FilePath, FileMode.Create));
                //}


                uniqueFile = ProcessingFileUploadMethod(obj, uniqueFile);
                Employee employee = new Employee()
                {
                    Name = obj.Name,
                    Emial = obj.Emial,
                    Departments = obj.Departments,
                    imagePath = uniqueFile
                };
                _repository.AddNewEmployee(employee);
                return RedirectToAction("Detail", new { id = employee.ID });

            }
            else
            {
                return View();
            }
           
        }

        [HttpGet]
       public ViewResult Edit(int id)
        {
            Employee obj = _repository.GetEmployeeById(id);

            if(obj!=null)
            {
                EditEmployeeViewModel model = new EditEmployeeViewModel
                {
                    ID = obj.ID,
                    Name = obj.Name,
                    Departments = obj.Departments,
                    Emial = obj.Emial,
                    ExistingPhoto = obj.imagePath
                };


                return View(model);
            }
            else
            {
                return View();
            }
        }


        [HttpPost]
        public IActionResult Edit(EditEmployeeViewModel obj)
        {
            Employee model = _repository.GetEmployeeById(obj.ID);
            if (ModelState.IsValid)
            {
                string uniqueFile = null;


                model.Name = obj.Name;
                model.Emial = obj.Emial;
                model.Departments = obj.Departments;
                if(obj.imagePath!=null)
                {
                    if(obj.ExistingPhoto!=null)
                    {
                        string filepath = Path.Combine(_hostingEnvironment.WebRootPath, "images", obj.ExistingPhoto);

                        System.IO.File.Delete(filepath);
                    }
                    model.imagePath = ProcessingFileUploadMethod(obj, uniqueFile);
                }
                
                _repository.UpdateEmployee(model);

                return RedirectToAction("ShowAllEmployee");

            }
            else
            {
                return View();
            }
        }

        private string ProcessingFileUploadMethod(EmployeesViewModel obj, string uniqueFile)
        {
            if (obj.imagePath != null && obj.imagePath.Count > 0)
            {
                foreach (IFormFile item in obj.imagePath)
                {
                    string UploadsFolders = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                    uniqueFile = Guid.NewGuid().ToString() + "_" + item.FileName;
                    string FilePath = Path.Combine(UploadsFolders, uniqueFile);
                    using (var fileStream = new FileStream(FilePath, FileMode.Create))
                    {
                        item.CopyTo(fileStream);
                    }
                }
            }
            return uniqueFile;
        }
    }
}
