using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VenketCorePracticeCore.Models;

namespace VenketCorePracticeCore.Controllers
{
    //[Route("[controller]/[action]")]
    public class EmplpyeeController:Controller
    {
        private readonly IEmployeeRepository employeeRepository;

        public EmplpyeeController(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        //[Route("~/Employee")]
       
        //[Route("~/")]
        public IActionResult Index()
        {
            return View(this.employeeRepository.GetAllEmployeesList());
        }

        public IActionResult Contact()
        {
            throw new Exception("Some thing is went Wrong");
        }
    }
}
