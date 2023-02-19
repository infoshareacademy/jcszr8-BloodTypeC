using BloodTypeC.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BloodTypeC.WebApp.Controllers
{
    public class FavoriteBeersController : Controller
    {
        // GET: FavoriteBeersController
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddToFavorites(int id)
        {
            var beerToFavs = DB.AllBeers.FirstOrDefault(x => x.Id == id.ToString());
            DB.FavoriteBeers.Add(beerToFavs);
            return RedirectToAction("AllBeers", "Home");
        }
        public ActionResult RemoveFromFavorites(int id)
        {
            var beerToFavs = DB.AllBeers.FirstOrDefault(x => x.Id == id.ToString());
            DB.FavoriteBeers.Remove(beerToFavs);
            return RedirectToAction("Favorites");
        }
        public ActionResult Favorites()
        {
            return View(DB.FavoriteBeers);
        }

        // GET: FavoriteBeersController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: FavoriteBeersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FavoriteBeersController/Create
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

        // GET: FavoriteBeersController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FavoriteBeersController/Edit/5
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

        // GET: FavoriteBeersController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FavoriteBeersController/Delete/5
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
