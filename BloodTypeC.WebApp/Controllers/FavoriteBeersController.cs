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

            return RedirectToAction(referer.Action, referer.Controller, referer);
        }

        public IActionResult RemoveFromFavorites(int id)
        {
            _favoriteBeersServices.RemoveFromFavs(id);
            var referer = GetReferer();

            return RedirectToAction(referer.Action, referer.Controller, referer);
        }

        private Referer GetReferer()
        {
            var result = new Referer();
            var host = Request.Host.ToUriComponent();
            var referer = Request.Headers.Referer.ToString() ?? string.Empty;
            var trimmedUrl = referer.Substring(referer.IndexOf(host) + host.Length + 1);
            string[] path = trimmedUrl.Split('/');
            result.Controller = path[0];
            result.Action = path[1];
            if (path.Length > 2)
            {
                result.Id = path[2];
            }
            return result;
        }
        private class Referer
        {
            public string Controller { get; set; }
            public string Action { get; set; }
            public string Id { get; set; }
        }
    }

}
