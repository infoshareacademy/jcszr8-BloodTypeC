using BloodTypeC.WebApp.Models;
using BloodTypeC.DAL;
using BloodTypeC.Logic;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;

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
        public IActionResult SearchByName(string searchBrewery, string searchBeerName)
        {
            var resultList = DB.AllBeers;
            if (!string.IsNullOrWhiteSpace(searchBrewery))
            {
                resultList = BeerOperations.SearchByBrewery(resultList, searchBrewery);               
            }
            if (!string.IsNullOrWhiteSpace(searchBeerName))
            {
                resultList = BeerOperations.SearchByName(resultList, searchBeerName);                
            }          
            return View(resultList);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}