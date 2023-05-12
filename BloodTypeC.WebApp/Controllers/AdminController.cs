using BloodTypeC.DAL.Models;
using BloodTypeC.DAL.Models.Views;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Data;

namespace BloodTypeC.WebApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            var model = new AdminPanelViewModel();
            model.Users = _userManager.Users;
            model.Roles= _roleManager.Roles;
            return View(model);
        }
        public IActionResult Create() 
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(User user)
        {
            if(ModelState.IsValid)
            {
                User appUser = new User();

                appUser.UserName = user.Email;
                appUser.Email = user.Email;
                //appUser.EmailConfirmed = true;

                IdentityResult result = await _userManager.CreateAsync(appUser, user.PasswordHash);
                if (result.Succeeded) 
                {
                    return RedirectToAction("Index");
                }
            }
            return View(user);
        }

        // TODO
        public ActionResult Edit(string id)
        {
            return View();
        }

        //TODO
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        //TODO
        public ActionResult Delete(string id)
        {
            return View();
        }

        //TODO
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        //TODO
        public ActionResult ActivateUser(string id)
        {
            return View();
        }

        //TODO
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ActivateUser(string id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
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
        public async Task<IActionResult> EditRole(string id,IdentityRole role)
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
            //var result = await _roleManager.DeleteAsync(roleToDelete);
            //if (result.Succeeded)
            //{
             //   return RedirectToAction("Index");
            //}
            return View(roleToDelete);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteRole(string id,IdentityRole role)
        {
            var roleToDelete = await _roleManager.FindByIdAsync(id);
            var result = await _roleManager.DeleteAsync(roleToDelete);
            if (result.Succeeded)
            {
               return RedirectToAction("Index");
            }
            return View(role);
        }
    }
}
