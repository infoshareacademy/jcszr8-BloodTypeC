using BloodTypeC.DAL.Models;
using BloodTypeC.Logic.Services.IServices;
using BloodTypeC.WebApp.Models;
using BloodTypeC.WebApp.WebExtensions;
using Microsoft.AspNetCore.Mvc;
using static BloodTypeC.DAL.Models.Enums.Enums;

namespace BloodTypeC.WebApp.Controllers
{
    public class BeerSearchController : Controller
    {
        private static List<FlavorToSearch> _flavorsToSearch;
        private static List<string> _allFlavors;
        private readonly IBeerServices _beerServices;
        private readonly IBeerSearchServices _beerSearchServices;
        private readonly IUserActivityServices _userActivityServices;
        public BeerSearchController(IBeerServices beerServices, IBeerSearchServices beerSearchServices, IUserActivityServices userActivityServices) 
        {
            _beerServices = beerServices;
            _beerSearchServices = beerSearchServices;
            _flavorsToSearch = new List<FlavorToSearch>();
            _allFlavors = beerSearchServices.GetAllFlavors(_beerServices.GetAll().Result.ToList());
            _userActivityServices = userActivityServices;
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
            model.Beers = _beerServices.GetAll().Result.ToList();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Index(IndexViewModel model)
        {
            var minimumAlcohol = 0.0;
            var maximumAlcohol = double.MaxValue;
            var resultList = _beerServices.GetAll().Result.ToList();

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.MinAbv.HasValue && model.MaxAbv.HasValue && model.MinAbv.Value > model.MaxAbv.Value)
            {
                ModelState.AddModelError(nameof(model.MinAbv), "Minimum value has to be lower than maximum value.");
                return View(model);
            }

            //filtering by alcohol volume
            if (model.MinAbv.HasValue)
            {
                minimumAlcohol = (double)model.MinAbv;
            }
            if (model.MaxAbv.HasValue)
            {
                maximumAlcohol = (double)model.MaxAbv;
            }

            //filtring by brewery name
            if (!string.IsNullOrWhiteSpace(model.SearchBrewery))
            {
                resultList = _beerSearchServices.SearchByBrewery(resultList, model.SearchBrewery);
            }
            //filtering by beer name
            if (!string.IsNullOrWhiteSpace(model.SearchBeerName))
            {
                resultList = _beerSearchServices.SearchByName(resultList, model.SearchBeerName);
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
                    tmpResultList.AddRange(_beerSearchServices.SearchByFlavor(resultList, flavor));
                }
                resultList = tmpResultList.Distinct().ToList();
            }

            resultList = _beerSearchServices.SearchByAlcVol(resultList, minimumAlcohol, maximumAlcohol);
            model.Beers = resultList;
            
            var userActivityTemplate = this.CreateUserActivityWithUserConnectionInfo();
            var filteredBeers = model.Beers.Select(x => x.Name).Aggregate((concat, str) => $"{concat} {str} ");
            var userActivity = await _userActivityServices.CreateUserActivity(userActivityTemplate, User.Identity.Name,
                UserActions.SearchForBeer, filteredBeers);
            await _userActivityServices.AddUserActivityAsync(userActivity);

            return View(model);
        }    
    }
}
