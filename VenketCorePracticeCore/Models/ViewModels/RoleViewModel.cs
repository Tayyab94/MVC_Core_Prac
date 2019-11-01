using System.ComponentModel.DataAnnotations;

namespace VenketCorePracticeCore.Models.ViewModels
{
    public class RoleViewModel
    {

        [Required]
        public string RoleName { get; set; }
    }
}