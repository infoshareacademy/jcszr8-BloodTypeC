using BloodTypeC.DAL;
using BloodTypeC.Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Primitives;

namespace BloodTypeC.WebApp.Controllers
{
    public class FavoriteBeersController : Controller
    {
        // GET: FavoriteBeersController
        public IActionResult Index()
        {
            return View();
        }
        
        [HttpGet]
        public IActionResult AddToFavorites(int id)
        {
            var referer = Request.Headers.Referer.ToString();
            BeerOperations.AddToFavs(id);
            if (referer.Contains("Details"))
            {
                return RedirectToAction("Details", "Home", new { id });
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public IActionResult RemoveFromFavorites(int id)
        {
            var referer = Request.Headers.Referer.ToString();
            BeerOperations.RemoveFromFavs(id);
            if (referer.Contains("Favorites"))
            {
                return RedirectToAction("Favorites");
            }
            if (referer.Contains("Details"))
            {
                return RedirectToAction("Details", "Home", new { id });
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public IActionResult Favorites()
        {
            return View(DB.FavoriteBeers);
        }

        //// GET: FavoriteBeersController/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: FavoriteBeersController/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: FavoriteBeersController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: FavoriteBeersController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: FavoriteBeersController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: FavoriteBeersController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: FavoriteBeersController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
