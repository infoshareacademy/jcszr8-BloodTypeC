using BloodTypeC.DAL.Models;
using BloodTypeC.Logic;
using BloodTypeC.WebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BloodTypeC.WebApp.Controllers
{
    public class SearchController : Controller
    {
        private static List<FlavorToSearch> _flavorsToSearch;
        private static List<Beer> _allBeers;
        public SearchController() 
        {
            _flavorsToSearch = new List<FlavorToSearch>();
            _allBeers = DB.AllBeers;
        }
        // GET: SearchController
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

        // GET: SearchController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SearchController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SearchController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SearchController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SearchController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SearchController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SearchController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
