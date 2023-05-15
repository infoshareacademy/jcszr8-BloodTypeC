using AutoMapper;
using BloodTypeC.DAL.Models;
using BloodTypeC.DAL.Models.Views;
using BloodTypeC.Logic.Services.IServices;
using BloodTypeC.WebApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Packaging;
using System.Security.Claims;
using System.Security.Policy;
using static BloodTypeC.Logic.Extensions.HttpContextExtensions;

namespace BloodTypeC.WebApp.Controllers
{
    public class FavoriteBeersController : Controller
    {
        private readonly IFavoriteBeersServices _favoriteBeersServices;
        private readonly IMapper _mapper;
        public FavoriteBeersController(IFavoriteBeersServices favoriteBeersServices, IMapper mapper)
        {
            _favoriteBeersServices = favoriteBeersServices;
            _mapper = mapper;
        }
        [HttpGet]

        public async Task<IActionResult> Favorites()
        {
            var userName = User.Identity.Name;
            var userFavorites = await _favoriteBeersServices.GetAllFavs(userName);
            var model = new FavoriteBeersViewModel();
            model.FavoriteBeers.AddRange(userFavorites);
            return View(model);
        }

        public async Task<IActionResult> AddToFavorites(string id)
        {
            var userName = User.Identity.Name;
            await _favoriteBeersServices.AddToFavs(id, userName);

            return RedirectToAction(HttpContext.GetController(), HttpContext.GetAction(), new { id });
        }

        public async Task<IActionResult> RemoveFromFavorites(string id)
        {
            var userName = User.Identity.Name;
            await _favoriteBeersServices.RemoveFromFavs(id, userName);

            return RedirectToAction(HttpContext.GetController(), HttpContext.GetAction(), new { id });
        }
    }


}
