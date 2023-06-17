using BloodTypeC.Logic.Services.IServices;
using BloodTypeC.WebApp.Models;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Globalization;

namespace BloodTypeC.WebApp.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly ILogger<HomeController> _logger;
        private static List<FlavorToSearch> _flavorsToSearch;
        private readonly IBeerServices _beerServices;

        public HomeController(ILogger<HomeController> logger, IBeerServices beerServices)
        {
            _logger = logger;
            _flavorsToSearch = new List<FlavorToSearch>();
            _beerServices = beerServices;
        }

        public IActionResult Index()
        {
            var model = new IndexViewModel();
            model.CheckedListOfFlavors = _flavorsToSearch;
            model.Beers = _beerServices.GetAll().Result.ToList();
            return View(model);        
        }
        
        public IActionResult ChangeLanguage(string culture)
        {
               Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
                   CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                   new CookieOptions() { Expires = DateTimeOffset.UtcNow.AddYears(1)});

            return Redirect(Request.Headers["Referer"].ToString());
        }

        public IActionResult AgeCheck()
        {
            return View();
        }

        public IActionResult GandalfProtocol()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new AllBeersViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}