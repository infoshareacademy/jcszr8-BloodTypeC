using BloodTypeC.WebApp.Models;
using BloodTypeC.DAL;
using BloodTypeC.Logic;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BloodTypeC.WebApp.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AllBeers()
        {
            return View(DB.AllBeers);
        }
        public IActionResult Favorites()
        {
            return View(DB.Favorites);
        }
        public IActionResult AddToFavorites()
        {
            return View(DB.AllBeers);
        }
        public IActionResult SearchByName(string searchName)
        {
            if (!string.IsNullOrWhiteSpace(searchName))
            {
                var result = BeerOperations.SearchByName(DB.AllBeers, searchName);
                return View(result);
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
            
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}