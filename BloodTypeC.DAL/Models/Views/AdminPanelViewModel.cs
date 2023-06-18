using Microsoft.AspNetCore.Identity;

namespace BloodTypeC.DAL.Models.Views
{
    public class AdminPanelViewModel
    {
        public IEnumerable<User> Users { get; set; }
        public IEnumerable<IdentityRole> Roles { get; set; }
    }
}
