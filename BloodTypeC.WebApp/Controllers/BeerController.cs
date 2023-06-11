using AutoMapper;
using BloodTypeC.DAL.Models;
using BloodTypeC.DAL.Repository;
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
        private readonly IRepository<UserActivity> _userActivityRepository;
        private readonly UserManager<User> _userManager;
        public BeerController(IBeerServices beerServices, IMapper mapper, IRepository<UserActivity> userActivityRepository, UserManager<User> userManager)
        {
            _beerServices = beerServices;
            _mapper= mapper;
            _userActivityRepository = userActivityRepository;
            _userManager= userManager;
        }

        // GET: BeerController/Details/5
        public async Task<ActionResult> Details(string id)
        {
            var model = await _beerServices.GetById(id);
            var newBeerDto = _mapper.Map<BeerViewModel>(model);
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
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var userAgent = Request.Headers.UserAgent.ToString();
            var userIP = HttpContext.Connection.RemoteIpAddress?.ToString();
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(beerFromView);
                }

                await _beerServices.AddFromView(beerFromView, user);
                await AddUserActivity(new UserActivity()
                                        {
                                            IPAddress = userIP,
                                            User = user,
                                            UserAction = UserActions.AddBeer,
                                            UserAgent = userAgent,
                                        });

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
                await _beerServices.Delete(id);
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
            }
        }
        private async Task AddUserActivity(UserActivity userActivity)
        {
            await _userActivityRepository.Insert(userActivity);
        }
    }
}
