using BloodTypeC.DAL;
using BloodTypeC.Logic;
using BloodTypeC.WebApp.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Primitives;

namespace BloodTypeC.WebApp.Controllers
{
    public class FavoriteBeersController : Controller
    {
        private readonly IFavoriteBeersServices _favoriteBeersServices;
        public FavoriteBeersController(IFavoriteBeersServices favoriteBeersServices)
        {
            _favoriteBeersServices = favoriteBeersServices;
        }
        // GET: FavoriteBeersController
        public IActionResult Index()
        {
            return View();
        }
        
        [HttpGet]
        public IActionResult AddToFavorites(int id)
        {
            var referer = Request.Headers.Referer.ToString();

            _favoriteBeersServices.AddToFavs(id);
            if (referer.Contains("Details"))
            {
                return RedirectToAction("Details", "Beer", new { id });
            }
            if (referer.Contains("AllBeers"))
            {
                return RedirectToAction("AllBeers", "Home", new { id });
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public IActionResult RemoveFromFavorites(int id)
        {
            var referer = Request.Headers.Referer.ToString();
            _favoriteBeersServices.RemoveFromFavs(id);
            if (referer.Contains("Favorites"))
            {
                return RedirectToAction("Favorites");
            }
            if (referer.Contains("AllBeers"))
            {
                return RedirectToAction("AllBeers", "Home", new { id });
            }
            if (referer.Contains("Details"))
            {
                return RedirectToAction("Details", "Beer", new { id });
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public IActionResult Favorites()
        {
            return View(_favoriteBeersServices.GetAllFavs());
        }
    }
}
