using AutoMapper;
using BloodTypeC.DAL.Models;
using BloodTypeC.Logic.Services.IServices;
using BloodTypeC.WebApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static BloodTypeC.DAL.Models.Enums.Enums;

namespace BloodTypeC.WebApp.Controllers
{
    public class BeerController : Controller
    {
        private readonly IBeerServices _beerServices;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IUserActivityServices _userActivityServices;
        public BeerController(IBeerServices beerServices, IMapper mapper, UserManager<User> userManager, IUserActivityServices userActivityServices)
        {
            _beerServices = beerServices;
            _mapper = mapper;
            _userManager = userManager;
            _userActivityServices = userActivityServices;
        }


        // GET: BeerController/Details/5
        public async Task<ActionResult> Details(string id)
        {
            var model = await _beerServices.GetById(id);
            var newBeerDto = _mapper.Map<BeerViewModel>(model);

            var userActivityTemplate = CreateUserActivityWithUserConnectionInfo();
            var userActivity = await _userActivityServices.CreateUserActivity(userActivityTemplate, User.Identity.Name,
                UserActions.ViewBeer, newBeerDto.Name);
            await _userActivityServices.AddUserActivityAsync(userActivity);

            return View(newBeerDto);
        }

        // GET: BeerController/Create
        public ActionResult Create()
        {
            var newBeer = new Beer();
            newBeer.Flavors = new List<string>() { "" };
            var newBeerDto = _mapper.Map<BeerViewModel>(newBeer);

            return View(newBeerDto);
        }

        // POST: BeerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(BeerViewModel beerFromView)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(beerFromView);
                }
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                await _beerServices.AddFromView(beerFromView, user);

                var userActivityTemplate = CreateUserActivityWithUserConnectionInfo();
                var userActivity = await _userActivityServices.CreateUserActivity(userActivityTemplate, user.UserName,
                    UserActions.AddBeer, beerFromView.Name);
                await _userActivityServices.AddUserActivityAsync(userActivity);

                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
            }
        }


        // GET: BeerController/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            var model = await _beerServices.GetById(id);
            var newBeerDto = _mapper.Map<BeerViewModel>(model);
            return View(newBeerDto);
        }

        // POST: BeerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(BeerViewModel model, IFormCollection collection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }
                var beerDto = _mapper.Map<BeerViewModel>(model);
                await _beerServices.EditFromView(beerDto);

                var userActivityTemplate = CreateUserActivityWithUserConnectionInfo();
                var userActivity = await _userActivityServices.CreateUserActivity(userActivityTemplate, User.Identity.Name,
                    UserActions.EditBeer, beerDto.Name);
                await _userActivityServices.AddUserActivityAsync(userActivity);


                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
            }
        }

        // GET: BeerController/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            var model = await _beerServices.GetById(id);
            var newBeerDto = _mapper.Map<BeerViewModel>(model);
            return View(newBeerDto);
        }

        // POST: BeerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(string id, IFormCollection collection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                var userActivityTemplate = CreateUserActivityWithUserConnectionInfo();
                var userActivity = await _userActivityServices.CreateUserActivity(userActivityTemplate, User.Identity.Name,
                    UserActions.RemoveBeer, _beerServices.GetById(id).Result.Name);
                await _userActivityServices.AddUserActivityAsync(userActivity);

                await _beerServices.Delete(id);
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
            }
        }
        private UserActivity CreateUserActivityWithUserConnectionInfo()
        {
            var userActivity = new UserActivity()
            {
                IPAddress = HttpContext.Connection.RemoteIpAddress?.ToString(),
                UserAgent = Request.Headers.UserAgent.ToString(),
            };
            return userActivity;
        }
    }
}
