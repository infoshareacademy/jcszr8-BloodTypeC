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
        // GET: BeerController
        public ActionResult Index()
        {
            return View();
        }

        // GET: BeerController/Details/5
        public ActionResult Details(int id)
        {
            var model = _beerServices.GetById(id);
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
        public ActionResult Create(BeerViewModel beerFromView)
        {
            
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(beerFromView);
                }
                _beerServices.AddFromView(beerFromView);
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
            }
        }

        // GET: BeerController/Edit/5
        public ActionResult Edit(int id)
        {
            var model = _beerServices.GetById(id);
            var newBeerDto = _mapper.Map<BeerViewModel>(model);
            return View(newBeerDto);
        }

        // POST: BeerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BeerViewModel model, IFormCollection collection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }
                var beerDto = _mapper.Map<BeerViewModel>(model);
                _beerServices.EditFromView(beerDto);
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
            }
        }

        // GET: BeerController/Delete/5
        public ActionResult Delete(int id)
        {
            var model = _beerServices.GetById(id);
            var newBeerDto = _mapper.Map<BeerViewModel>(model);
            return View(newBeerDto);
        }

        // POST: BeerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }
                _beerServices.Delete(id);
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
            }
        }
    }
}
