using AutoMapper;
using BloodTypeC.DAL.Models;
using BloodTypeC.Logic.Services.IServices;
using BloodTypeC.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace BloodTypeC.WebApp.Controllers
{
    public class BeerController : Controller
    {
        private readonly IBeerServices _beerServices;
        private readonly IMapper _mapper;
        public BeerController(IBeerServices beerServices, IMapper mapper)
        {
            _beerServices = beerServices;
            _mapper= mapper;
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
            
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(beerFromView);
                }
                await _beerServices.AddFromView(beerFromView, User.Identity.Name);
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
    }
}
