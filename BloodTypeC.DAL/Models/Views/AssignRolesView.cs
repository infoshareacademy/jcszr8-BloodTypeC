using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodTypeC.DAL.Models.Views
{
    public class AssignRolesView
    {
        public User User { get; set; }
        public List<string> UserRoles { get; set; }
        public IEnumerable<IdentityRole> AvalaibleRolesToAssign { get; set; }
        public IEnumerable<string> RolesIdToAssign { get; set; }
    }
}
