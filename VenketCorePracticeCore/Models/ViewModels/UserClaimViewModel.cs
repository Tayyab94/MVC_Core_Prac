using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VenketCorePracticeCore.Models.ViewModels
{
    public class UserClaimViewModel
    {

        public UserClaimViewModel()
        {
            Claims = new List<UserClaims>();
        }
        public string UserId { get; set; }

        public List<UserClaims> Claims{ get; set; }
    }
}
