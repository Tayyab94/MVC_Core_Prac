using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VenketCorePracticeCore.Models.ViewModels
{
    public class loginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }


        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Display(Name ="Remember Me")]
        public bool Rememberme { get; set; }


        public string ReturnURL { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }
    }
}
