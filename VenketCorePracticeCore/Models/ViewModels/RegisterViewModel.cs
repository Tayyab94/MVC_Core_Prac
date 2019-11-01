using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;



namespace VenketCorePracticeCore.Models.ViewModels
{
    public class RegisterViewModel
    {

        [Required]
        [EmailAddress]
        [Remote(action: "IsEmailExist", controller:"Access")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and Confirm Password don't match")]

        public string ConfirmPassword { get; set; }

        [Display(Name ="Nick Name")]
        public string FirstNameofUser { get; set; }
    }

    public class RegisterUsers:RegisterViewModel
    {
        public string ID { get; set; }
        public string UserNam { get; set; }
    }
}
