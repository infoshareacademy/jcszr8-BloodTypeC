using BloodTypeC.WebApp.Models;
using BloodTypeC.Logic;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Drawing.Text;
using System.Reflection;
using BloodTypeC.DAL.Models;

namespace BloodTypeC.WebApp.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly ILogger<HomeController> _logger;
        private static List<FlavorToSearch> _flavorsToSearch;
        private static List<Beer> _allBeers;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _flavorsToSearch = new List<FlavorToSearch>();
            _allBeers = DB.AllBeers;
        }

        public IActionResult Index()
        {
            var model = new IndexViewModel();
            model.CheckedListOfFlavors = _flavorsToSearch;
            model.Beers = _allBeers;
            return View(model);        
        }

        public IActionResult Details(int id)
        {
            var beerToDisplay = DB.AllBeers.FirstOrDefault(x => x.Id == id.ToString());
            return View(beerToDisplay);
        }
                public IActionResult AgeCheck()
        {
            return View();
        }

        public IActionResult GP()
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