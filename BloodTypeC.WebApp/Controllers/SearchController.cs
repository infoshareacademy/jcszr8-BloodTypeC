using BloodTypeC.DAL.Models;
using BloodTypeC.Logic.Services;
using BloodTypeC.Logic.Services.IServices;
using BloodTypeC.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace BloodTypeC.WebApp.Controllers
{
    public class SearchController : Controller
    {
        private static List<FlavorToSearch> _flavorsToSearch;
        private static List<string> _allFlavors;
        private readonly IBeerServices _beerServices;
        private readonly IBeerSearchServices _beerSearchServices;
        public SearchController(IBeerServices beerServices, IBeerSearchServices beerSearchServices) 
        {
            _beerServices = beerServices;
            _flavorsToSearch = new List<FlavorToSearch>();
            _allFlavors = beerSearchServices.GetAllFlavors(_beerServices.GetAll().ToList());
        }
        // GET: SearchController
        public IActionResult Index()
        {
            
            foreach (var flavor in _allFlavors)
            {
                var activeFlavor = new FlavorToSearch() { Name = flavor, IsChecked = false };
                if (!_flavorsToSearch.Contains(activeFlavor))
                {
                    _flavorsToSearch.Add(activeFlavor);
                }
            }
            var model = new IndexViewModel();
            model.CheckedListOfFlavors = _flavorsToSearch;
            model.Beers = _beerServices.GetAll().ToList();
            return View(model);
        }
        [HttpPost]
        public IActionResult Index(IndexViewModel model)
        {
            var minimumAlcohol = 0.0;
            var maximumAlcohol = double.MaxValue;
            var resultList = _beerServices.GetAll().ToList();

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.minAbv.HasValue && model.maxAbv.HasValue && model.minAbv.Value > model.maxAbv.Value)
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
                resultList = BeerSearchServices.SearchByBrewery(resultList, model.searchBrewery);
            }
            //filtering by beer name
            if (!string.IsNullOrWhiteSpace(model.searchBeerName))
            {
                resultList = BeerSearchServices.SearchByName(resultList, model.searchBeerName);
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
                    tmpResultList.AddRange(BeerSearchServices.SearchByFlavor(resultList, flavor));
                }
                resultList = tmpResultList.Distinct().ToList();
            }

            resultList = BeerSearchServices.SearchByAlcVol(resultList, minimumAlcohol, maximumAlcohol);
            model.Beers = resultList;
            return View(model);
        }    
    }
}
