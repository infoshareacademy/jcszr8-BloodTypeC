using AutoMapper;
using BloodTypeC.DAL.Models.Views;
using BloodTypeC.Logic.Services;
using BloodTypeC.Logic.Services.IServices;
using BloodTypeC.WebApp.WebExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Packaging;
using static BloodTypeC.DAL.Models.Enums.Enums;
using static BloodTypeC.Logic.Extensions.HttpContextExtensions;

namespace BloodTypeC.WebApp.Controllers
{
    [Authorize]
    public class FavoriteBeersController : Controller
    {
        private readonly IFavoriteBeersServices _favoriteBeersServices;
        private readonly IBeerServices _beerServices;
        private readonly IUserActivityServices _userActivityServices;
        public FavoriteBeersController(IFavoriteBeersServices favoriteBeersServices, IBeerServices beerServices, IUserActivityServices userActivityServices)
        {
            _favoriteBeersServices = favoriteBeersServices;
            _beerServices = beerServices;
            _userActivityServices = userActivityServices;
        }

        [HttpGet]
        public async Task<IActionResult> Favorites()
        {
            var userName = User.Identity.Name;
            var userFavorites = await _favoriteBeersServices.GetAllFavs(userName);
            var model = new FavoriteBeersViewModel();
            model.FavoriteBeers.AddRange(userFavorites);

            var userActivityTemplate = this.CreateUserActivityWithUserConnectionInfo();
            var favoriteBeers = model.FavoriteBeers.Select(x => x.Name).Aggregate((concat, str) => $"{concat} {str} ");
            var userActivity = await _userActivityServices.CreateUserActivity(userActivityTemplate, User.Identity.Name,
                UserActions.ViewFavorites, favoriteBeers);
            await _userActivityServices.AddUserActivityAsync(userActivity);

            return View(model);
        }

        public async Task<IActionResult> AddToFavorites(string id)
        {
            var userName = User.Identity.Name;
            await _favoriteBeersServices.AddToFavs(id, userName);

            var userActivityTemplate = this.CreateUserActivityWithUserConnectionInfo();
            var beer = await _beerServices.GetById(id);
            var userActivity = await _userActivityServices.CreateUserActivity(userActivityTemplate, User.Identity.Name,
                UserActions.AddBeerToFavorites, beer.Name);
            await _userActivityServices.AddUserActivityAsync(userActivity);

            return RedirectToAction(HttpContext.GetController(), HttpContext.GetAction(), new { id });
        }

        public async Task<IActionResult> RemoveFromFavorites(string id)
        {
            var userName = User.Identity.Name;
            await _favoriteBeersServices.RemoveFromFavs(id, userName);

            var userActivityTemplate = this.CreateUserActivityWithUserConnectionInfo();
            var beer = await _beerServices.GetById(id);
            var userActivity = await _userActivityServices.CreateUserActivity(userActivityTemplate, User.Identity.Name,
                UserActions.RemoveBeerFromFavorites, beer.Name);
            await _userActivityServices.AddUserActivityAsync(userActivity);

            return RedirectToAction(HttpContext.GetController(), HttpContext.GetAction(), new { id });
        }
    }


}
