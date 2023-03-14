using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BloodTypeC.DAL;
using BloodTypeC.WebApp.Services;
using BloodTypeC.WebApp.Services.IServices;
using BloodTypeC.Logic;

namespace BloodTypeC.WebApp.Controllers
{
    public class BeerController : Controller
    {
        private readonly IBeerServices _beerServices;
        public BeerController(IBeerServices beerServices)
        {
            _beerServices= beerServices;
        }
        // GET: BeerController
        public ActionResult Index()
        {
            return View();
        }

        // GET: BeerController/Details/5
        public ActionResult Details(int id)
        {
            var model = _beerServices.GetById(id);
            return View(model);
        }

        // GET: BeerController/Create
        public ActionResult Create()
        {
            Beer newBeer = new();
            return View(newBeer);
        }

        // POST: BeerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Beer beerToAdd)
        {
            ModelState.Remove(nameof(beerToAdd.Id));
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(beerToAdd);
                }
                if (!string.IsNullOrWhiteSpace(beerToAdd.FlavorsString))
                {
                    beerToAdd.Flavors = Format.AsTags(beerToAdd.FlavorsString);
                }
                _beerServices.Add(beerToAdd);
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
            }
        }

        // GET: BeerController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BeerController/Edit/5
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

        // GET: BeerController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BeerController/Delete/5
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
