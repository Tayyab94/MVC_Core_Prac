using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VenketCorePracticeCore.Models.ViewModels
{
    public class EmployeesViewModel
    {

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Email format is NOT Correct")]
        public string Emial { get; set; }

        public Department Departments { get; set; }

        //public IFormFile imagePath { get; set; }  For single Image

        public List<IFormFile> imagePath { get; set; }   //Fro multiple Files
    }

    public class EditEmployeeViewModel : EmployeesViewModel
    {
        public int ID { get; set; }


        public string ExistingPhoto { get; set; }


    }
}
