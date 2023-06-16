// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using BloodTypeC.DAL.Models;
using BloodTypeC.DAL.Models.Enums;
using BloodTypeC.Logic.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;

namespace BloodTypeC.WebApp.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ResendEmailConfirmationModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly IMailService _mailService;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IUserActivityServices _userActivityServices;

        public ResendEmailConfirmationModel(UserManager<User> userManager, IMailService mailService, IHttpContextAccessor contextAccessor, IUserActivityServices userActivityServices)
        {
            _userManager = userManager;
            _mailService = mailService;
            _contextAccessor = contextAccessor;
            _userActivityServices = userActivityServices;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [EmailAddress]
            public string Email { get; set; }
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.FindByEmailAsync(Input.Email);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Verification email sent. Please check your email.");
                return Page();
            }

            var userId = await _userManager.GetUserIdAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var callbackUrl = Url.Page(
                "/Account/ConfirmEmail",
                pageHandler: null,
                values: new { userId = userId, code = code },
                protocol: Request.Scheme);
            await _mailService.SendAsync(new MailData(
                to: new List<string>() { Input.Email },
                subject: "Confirm your e-mail",
                body: $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.",
                from: Consts.mailSenderFrom,
                displayName: "Beeropedia"), new CancellationToken());

            var ip = _contextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
            var userActivityTemplate = new UserActivity() { IPAddress = ip, UserAgent = _contextAccessor.HttpContext.Request.Headers.UserAgent.ToString() };
            var userActivity = await _userActivityServices.CreateUserActivity(userActivityTemplate,
                Input.Email, Enums.UserActions.ResendConfirmationEmail);
            await _userActivityServices.LogUserActivityAsync(userActivity);


            ModelState.AddModelError(string.Empty, "Verification email sent. Please check your email.");
            return Page();
        }
    }
}
