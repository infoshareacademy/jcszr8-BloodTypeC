using Microsoft.AspNetCore.Identity;

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
