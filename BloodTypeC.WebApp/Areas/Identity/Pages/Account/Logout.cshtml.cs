// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using BloodTypeC.DAL.Models;
using BloodTypeC.DAL.Models.Enums;
using BloodTypeC.Logic.Services.IServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BloodTypeC.WebApp.Areas.Identity.Pages.Account
{
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<LogoutModel> _logger;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IUserActivityServices _userActivityServices;

        public LogoutModel(SignInManager<User> signInManager, ILogger<LogoutModel> logger, IHttpContextAccessor contextAccessor, IUserActivityServices userActivityServices)
        {
            _signInManager = signInManager;
            _logger = logger;
            _contextAccessor = contextAccessor;
            _userActivityServices = userActivityServices;
        }

        public async Task<IActionResult> OnPost(string returnUrl = null)
        {

            var ip = _contextAccessor?.HttpContext?.Connection?.RemoteIpAddress?.ToString();
            var userActivityTemplate = new UserActivity() { IPAddress = ip, UserAgent = _contextAccessor?.HttpContext?.Request?.Headers?.UserAgent.ToString() };
            var userActivity = await _userActivityServices.CreateUserActivity(userActivityTemplate,
                _contextAccessor.HttpContext.User.Identity.Name, Enums.UserActions.LogOut, "Logout");
            await _userActivityServices.AddUserActivityAsync(userActivity);

            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                // This needs to be a redirect so that the browser performs a new
                // request and the identity for the user gets updated.
                return RedirectToPage();
            }
        }
    }
}
