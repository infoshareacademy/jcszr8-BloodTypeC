using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodTypeC.DAL.Models.Views
{
    public class AssigneRolesView
    {
        public User User { get; set; }
        public List<string> UserRoles { get; set; }
        public IEnumerable<IdentityRole> AvalaibleRolesToAssigne { get; set; }
        public IEnumerable<string> RolesIdToAssigne { get; set; }
    }
}
