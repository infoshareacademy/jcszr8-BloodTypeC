using BloodTypeC.DAL.Models;
using BloodTypeC.Logic.Services.IServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static BloodTypeC.Logic.Extensions.HttpContextExtensions;

namespace BloodTypeC.WebApp.Controllers
{
    public class FavoriteBeersController : Controller
    {
        private readonly IFavoriteBeersServices _favoriteBeersServices;
        private readonly string _userId;
        public FavoriteBeersController(IFavoriteBeersServices favoriteBeersServices)
        {
            _favoriteBeersServices = favoriteBeersServices;
            _userId = "da1ae07a-873f-4f8e-b922-c9cf9935d059";
        }
        [HttpGet]

        public IActionResult Favorites()
        {
            return View(_favoriteBeersServices.GetAllFavs(_userId));
        }

        public IActionResult AddToFavorites(string id)
        {
            _favoriteBeersServices.AddToFavs(id, _userId);

            return RedirectToAction(this.HttpContext.GetController(), this.HttpContext.GetAction(), new { id });
        }

        public IActionResult RemoveFromFavorites(string id)
        {
            _favoriteBeersServices.RemoveFromFavs(id, _userId);

            return RedirectToAction(this.HttpContext.GetController(), this.HttpContext.GetAction(), new { id });
        }
    }


}
