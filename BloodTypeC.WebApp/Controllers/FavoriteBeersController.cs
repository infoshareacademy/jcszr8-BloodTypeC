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

            // Returns the same view from which the method was called
            // but checks whether it's the Details view that needs the id
            if (referer.Value.All(ch => char.IsLetter(ch)))
            {
                return RedirectToAction(referer.Value, referer.Key);
            }
            else
            {
                return RedirectToAction("Details", "Beer", new { id });
            }
        }
        public IActionResult RemoveFromFavorites(int id)
        {
            _favoriteBeersServices.RemoveFromFavs(id);
            var referer = GetReferer();

            // Returns the same view from which the method was called
            // but checks whether it's the Details view that needs the id

            if (referer.Value.All(ch => char.IsLetter(ch)))
            {
                return RedirectToAction(referer.Value, referer.Key);
            }
            else
            {
                return RedirectToAction("Details", "Beer", new { id });
            }
        }
        public IActionResult Favorites()
        {
            return View(_favoriteBeersServices.GetAllFavs());
        }

        public KeyValuePair<string, string> GetReferer()
        {
            // Extracts the Controller and Action strings from the referer url
            // Key = Controller
            // Value = Action

            var host = Request.Host.ToUriComponent();
            var referer = Request.Headers.Referer.ToString() ?? string.Empty;
            var trimmedUrl = referer.Substring(referer.IndexOf(host) + host.Length + 1);
            var controller = trimmedUrl.Remove(trimmedUrl.IndexOf('/'));
            var action = trimmedUrl.Substring(trimmedUrl.LastIndexOf('/') + 1);
            var controllerAction = new KeyValuePair<string, string>(controller, action);
            return controllerAction;
        }
    }
}
