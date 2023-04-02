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

        [HttpGet]
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
        public IActionResult Favorites()
        {
            return View(_favoriteBeersServices.GetAllFavs());
        }

        public string[] GetReferer()
        {
            // Extracts the Controller and Action strings from the referer url
            // [0] = Controller
            // [1] = Action
            // [2] = Id (when available)

            var host = Request.Host.ToUriComponent();
            var referer = Request.Headers.Referer.ToString() ?? string.Empty;
            var trimmedUrl = referer.Substring(referer.IndexOf(host) + host.Length + 1);
            string[] path = trimmedUrl.Split('/');
            return path;
        }
    }
}
