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
        DB.AllFlavors = BeerOperations.GetAllFlavors();
        foreach (var flavor in DB.AllFlavors)
        {
            var activeFlavor = new FlavorToSearch() { Name = flavor, IsChecked = false };
            if (!_flavorsToSearch.Contains(activeFlavor))
            {
                _flavorsToSearch.Add(activeFlavor);
            }
        }
            var model = new IndexViewModel();
            model.CheckedListOfFlavors = _flavorsToSearch;
            model.Beers = _allBeers;
            return View(model);        
        }

        [HttpPost]
        public IActionResult Index(IndexViewModel model)
        {
            var minimumAlcohol = 0.0;
            var maximumAlcohol = double.MaxValue;
            var resultList = _allBeers;

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if(model.minAbv.HasValue && model.maxAbv.HasValue && model.minAbv.Value > model.maxAbv.Value)
            {
                ModelState.AddModelError(nameof(model.minAbv), "Minimum value has to be lower than maximum value.");
                return View(model);
            }

            //filtering by alcohol volume
            if (model.minAbv.HasValue)
            {
                minimumAlcohol = (double)model.minAbv;
            }
            if (model.maxAbv.HasValue)
            {
                maximumAlcohol = (double)model.maxAbv;
            }

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
            List<string> activeFlavors = new List<string>();
            foreach (var item in model.CheckedListOfFlavors)
            {
                if (item.IsChecked)
                {
                    activeFlavors.Add(item.Name);
                }
            }
            //filtering by flavors      
            if (activeFlavors.Count > 0)
            {
                var tmpResultList = new List<Beer>();
                foreach (var flavor in activeFlavors)
                {
                    tmpResultList.AddRange(BeerOperations.SearchByFlavor(resultList, flavor));
                }
                resultList = tmpResultList.Distinct().ToList();
            }

            resultList = BeerOperations.SearchByAlcVol(resultList, minimumAlcohol, maximumAlcohol);
            model.Beers = resultList;
            return View(model);
        }

            public IActionResult AllBeers()
        {
            return View(DB.AllBeers);
        }
        public IActionResult Details(int id)
        {
            var beerToDisplay = DB.AllBeers.FirstOrDefault(x => x.Id == id.ToString());
            return View(beerToDisplay);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}