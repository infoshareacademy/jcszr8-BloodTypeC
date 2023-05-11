using BloodTypeC.DAL.Models;
using BloodTypeC.DAL.Models.Views;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BloodTypeC.WebApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserManager<User> _userManager;

        public AdminController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var model = new AdminPanelViewModel();
            model.Users = _userManager.Users;
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
    }
}
