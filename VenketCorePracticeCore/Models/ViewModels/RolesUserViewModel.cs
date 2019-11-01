using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VenketCorePracticeCore.Models.ViewModels
{
    public class RolesUserViewModel
    {
        public string UserId { get; set; }

        public string RoleId { get; set; }

        public string RoleName { get; set; }

        public bool IsSelected { get; set; }
    }
}
