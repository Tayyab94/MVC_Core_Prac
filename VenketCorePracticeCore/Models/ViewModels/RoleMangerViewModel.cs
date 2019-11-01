using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VenketCorePracticeCore.Models.ViewModels
{
    public class RoleMangerViewModel
    {

        [Required]
        public string RoleName { get; set; }
    }
}
