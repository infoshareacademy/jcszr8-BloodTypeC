using BloodTypeC.WebApp.Models;
using BloodTypeC.DAL;
using BloodTypeC.Logic;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Drawing.Text;
using System.Reflection;

namespace BloodTypeC.WebApp.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(IndexViewModel model)
        {
            




            var resultList = DB.AllBeers;
            var minimumAlcohol = 0.0;
            var maximumAlcohol = double.MaxValue;
            //filtring by brewery name
            if (!string.IsNullOrWhiteSpace(model.searchBrewery))
            {
                resultList = BeerOperations.SearchByBrewery(resultList, model.searchBrewery);
            }
            //filtering by beer name
            if (!string.IsNullOrWhiteSpace(model.searchBeerName))
            {
                resultList = BeerOperations.SearchByName(resultList, model.searchBeerName);
            }
            /*model.searchFlavors = allFlavors;
            //filtering by flavors
            if (model.searchFlavors!=null)
            {
                if (model.searchFlavors.Count > 0)
                {
                    var tmpResultList = new List<Beer>();
                    foreach (var flavor in model.searchFlavors)
                    {
                        tmpResultList.AddRange(BeerOperations.SearchByFlavor(resultList, flavor));
                    }
                    resultList = tmpResultList.Distinct().ToList();
                }              
            }*/
            //filtering by alcohol volume
            if (model.minAbv.HasValue)
            {
                minimumAlcohol = (double)model.minAbv;
            }
            if (model.maxAbv.HasValue)
            {
                maximumAlcohol = (double)model.maxAbv;
            }
            resultList = BeerOperations.SearchByAlcVol(resultList, minimumAlcohol, maximumAlcohol);
            model.Beers = resultList;
            return View(model);        
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}