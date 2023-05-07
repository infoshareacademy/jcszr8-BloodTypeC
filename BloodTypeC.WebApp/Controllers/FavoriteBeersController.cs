using BloodTypeC.Logic.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using static BloodTypeC.Logic.Extensions.HttpContextExtensions;

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

        public IActionResult AddToFavorites(string id)
        {
            _favoriteBeersServices.AddToFavs(id);

            return RedirectToAction(this.HttpContext.GetController(), this.HttpContext.GetAction(), new { id });
        }

        public IActionResult RemoveFromFavorites(string id)
        {
            _favoriteBeersServices.RemoveFromFavs(id);

            return RedirectToAction(this.HttpContext.GetController(), this.HttpContext.GetAction(), new { id });
        }
    }


}
