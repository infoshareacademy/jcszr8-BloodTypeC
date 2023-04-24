﻿using BloodTypeC.Logic.Services.IServices;
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

        public IActionResult AddToFavorites(int id)
        {
            _favoriteBeersServices.AddToFavs(id);

            return RedirectToAction(this.HttpContext.GetAction(), this.HttpContext.GetController(), new { id });
        }

        public IActionResult RemoveFromFavorites(int id)
        {
            _favoriteBeersServices.RemoveFromFavs(id);

            return RedirectToAction(this.HttpContext.GetAction(), this.HttpContext.GetController(), new { id });
        }
    }


}
