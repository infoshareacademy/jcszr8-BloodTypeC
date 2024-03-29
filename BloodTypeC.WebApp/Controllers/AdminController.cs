﻿using BloodTypeC.DAL.Models;
using BloodTypeC.DAL.Models.Views;
using BloodTypeC.Logic.Extensions;
using BloodTypeC.Logic.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BloodTypeC.WebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserActivityServices _userActivityServices;
        private readonly IMailService _mailService;

        public AdminController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager,
            IUserActivityServices userActivityServices, IMailService mailService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _userActivityServices = userActivityServices;
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

        public IActionResult SimpleReport()
        {
            var model = new SimpleLogViewModel();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SimpleReport(SimpleLogViewModel model)
        {
            var userActivityLog = await _userActivityServices.GetLastUserActivityAsync(model.TargetUser);
            model.LastUserActivity = userActivityLog.UserAction.ToString();
            model.LastUserActivityTime = userActivityLog.Time;
            model.LastUserActivityObject = userActivityLog.ObjectName;
            model.UserLogIns = await _userActivityServices.CountUserLogInsAsync(model.TargetUser);
            model.UserLogOuts = await _userActivityServices.CountUserLogOutsAsync(model.TargetUser);


            return View(model);
        }

        public async Task<IActionResult> ActivityReport()
        {
            var model = new ActivityReportViewModel
            {
                TargetDate = DateTime.Today,
                UserActivities = await _userActivityServices.GetAllUserActivitiesAsync(),
                CustomDate = false
            };

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ActivityReport(ActivityReportViewModel model)
        {
            var filteredModel = await _userActivityServices.FilterActivities(model);
            model.UserActivities = filteredModel;
            return View(model);
        }

        public async Task<IActionResult> SendToEmail()
        {
            var options = await _userActivityServices.GetAdminReportsOptionsAsync(User.Identity.Name);
            var model = new ActivityReportViewModel()
            {
                TargetDate = DateTime.Today,
                UserActivities = await _userActivityServices.GetAllUserActivitiesAsync(),
                CustomDate = false,
                ReportsOptions = options
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendToEmail(ActivityReportViewModel model)
        {
            await _userActivityServices.SaveAdminReportsOptionsAsync(model.ReportsOptions);

            var filteredModel = await _userActivityServices.FilterActivities(model);
            model.UserActivities = filteredModel;

            var mailBody = string.Empty;
            foreach (var item in model.UserActivities)
            {
                mailBody += item.ToLogHtml();
            }

            if (model.UserActivities.Any())
            {
                var mail = _mailService.CreateMailTemplate("Beeropedia user activity report", mailBody,
                    "This mail was generated automatically by a Beeropedia reporting system. Do not reply to it.");

                await _userActivityServices.SendUserActivityToEmail(mail, model.ReportsOptions.SendTargetEmail);
            }

            return View(model);
        }


    }
}
