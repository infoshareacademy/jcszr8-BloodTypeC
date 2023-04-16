using BloodTypeC.Logic.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace BloodTypeC.WebApp.Controllers
{
    public class FavoriteBeersController : Controller
    {
        private readonly IFavoriteBeersServices _favoriteBeersServices;
        public FavoriteBeersController(IFavoriteBeersServices favoriteBeersServices)
        {
            _favoriteBeersServices = favoriteBeersServices;
        }
        [HttpGet]        
        
        public IActionResult Favorites()
        {
            return View(_favoriteBeersServices.GetAllFavs());
        }

        public IActionResult AddToFavorites(int id)
        {
            _favoriteBeersServices.AddToFavs(id);
            var referer = GetReferer();

            return RedirectToAction(referer[1], referer[0], new { id });
        }
        
        public IActionResult RemoveFromFavorites(int id)
        {
            _favoriteBeersServices.RemoveFromFavs(id);
            var referer = GetReferer();

            return RedirectToAction(referer[1], referer[0], new { id });
        }

        public string[] GetReferer()
        {
            var host = Request.Host.ToUriComponent();
            var referer = Request.Headers.Referer.ToString() ?? string.Empty;
            var trimmedUrl = referer.Substring(referer.IndexOf(host) + host.Length + 1);
            string[] path = trimmedUrl.Split('/');
            return path;
        }
    }
}
