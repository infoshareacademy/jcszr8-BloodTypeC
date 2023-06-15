using BloodTypeC.DAL.Models;
using BloodTypeC.DAL.Models.Views;
using BloodTypeC.Logic.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BloodTypeC.WebApp.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMailService _mailService;

        public AdminController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IMailService mailService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mailService = mailService;
        }

        public IActionResult Index()
        {
            var model = new AdminPanelViewModel();
            model.Users = _userManager.Users;
            model.Roles = _roleManager.Roles;
            return View(model);
        }
        public IActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUser(User user)
        {
            if (ModelState.IsValid)
            {
                User appUser = new User();

                appUser.UserName = user.Email;
                appUser.Email = user.Email;
                IdentityResult result = await _userManager.CreateAsync(appUser, user.PasswordHash);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(user);
        }
        public async Task<IActionResult> EditUser(string id)
        {
            var model = await _userManager.FindByIdAsync(id);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(string id, User user)
        {
            if (ModelState.IsValid)
            {
                var userToEdit = await _userManager.FindByIdAsync(id);
                userToEdit.UserName = user.UserName;
                userToEdit.Email = user.Email;
                userToEdit.EmailConfirmed = user.EmailConfirmed;
                var result = await _userManager.UpdateAsync(userToEdit);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(user);
        }
        public async Task<IActionResult> DeleteUser(string id)
        {
            var model = await _userManager.FindByIdAsync(id);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUser(User user)
        {
            var userToDelete = await _userManager.FindByIdAsync(user.Id);
            var result = await _userManager.DeleteAsync(userToDelete);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            return View(user);
        }
        public async Task<IActionResult> ActivateUser(string id)
        {
            var userToActivate = await _userManager.FindByIdAsync(id);
            if (userToActivate.EmailConfirmed == false)
            {
                userToActivate.EmailConfirmed = true;
                await _userManager.UpdateAsync(userToActivate);
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> DeactivateUser(string id)
        {
            var userToDeactivate = await _userManager.FindByIdAsync(id);
            if (userToDeactivate.EmailConfirmed == true)
            {
                userToDeactivate.EmailConfirmed = false;
                await _userManager.UpdateAsync(userToDeactivate);
            }
            return RedirectToAction("Index");
        }
        public IActionResult CreateRole()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRole(IdentityRole role)
        {
            if (ModelState.IsValid)
            {
                IdentityRole appRole = new IdentityRole();
                appRole = role;
                var result = await _roleManager.CreateAsync(appRole);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }
            return View();
        }
        public async Task<IActionResult> EditRole(string id)
        {
            var model = await _roleManager.FindByIdAsync(id);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditRole(IdentityRole role)
        {
            if (ModelState.IsValid)
            {
                var roleToUpdate = await _roleManager.FindByIdAsync(role.Id);
                roleToUpdate.Name = role.Name;
                var result = await _roleManager.UpdateAsync(roleToUpdate);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }
            return View();
        }
        public async Task<IActionResult> DeleteRole(string id)
        {
            var roleToDelete = await _roleManager.FindByIdAsync(id);
            return View(roleToDelete);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteRole(IdentityRole role)
        {
            var roleToDelete = await _roleManager.FindByIdAsync(role.Id);
            var result = await _roleManager.DeleteAsync(roleToDelete);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            return View(role);
        }

        public async Task<IActionResult> AssignUserRoles(string userId)
        {
            AssignRolesView model = new AssignRolesView();
            model.User = await _userManager.FindByIdAsync(userId);
            model.AvalaibleRolesToAssign = _roleManager.Roles;
            var userRoles = await _userManager.GetRolesAsync(model.User);
            model.UserRoles = userRoles.ToList();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignUserRoles(AssignRolesView model)
        {
            if (model.RolesIdToAssign != null)
            {
                foreach (var roleId in model.RolesIdToAssign)
                {
                    var user = await _userManager.FindByIdAsync(model.User.Id);
                    var roleToAssign = await _roleManager.FindByIdAsync(roleId);
                    if (roleToAssign != null)
                    {
                        await _userManager.AddToRoleAsync(user, roleToAssign.Name);
                    }
                }
                return RedirectToAction("Index");
            }
            return RedirectToAction("AssignUserRoles", new { userId = model.User.Id });
        }

        //[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveUserRole(string id, string roleName)
        {
            var user = await _userManager.FindByIdAsync(id);
            await _userManager.RemoveFromRoleAsync(user, roleName);
            return RedirectToAction("AssignUserRoles", new { userId = user.Id });
        }

        public async Task<IActionResult> SendMailAsync()
        {
            var mailData = new MailData(new List<string>() { "gdntnaktfskpallkam@bbitj.com" }, "test", "treść wiadomości",
                null, "display bloodtypec", "bloodtypec@wp.pl");
            await _mailService.SendAsync(mailData, new CancellationToken());

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ActivityLog()
        {
            var model = new ActivityLogViewModel();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> ActivityLog(ActivityLogViewModel model)
        {
            model.LastUserActivity = "dupa";
            model.LastUserActivityObject = "maryny";
            model.LastUserActivityTime = DateTime.Today;
            model.UserLogIns = 1;
            model.UserLogOuts = 14;
            return View(model);
        }
    }
}
