using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BloodTypeC.DAL;
using BloodTypeC.WebApp.Services;
using BloodTypeC.WebApp.Services.IServices;
using BloodTypeC.Logic;
using AutoMapper;
using BloodTypeC.WebApp.Models;
using BloodTypeC.DAL.Models;
using BloodTypeC.DAL.Repository;

namespace BloodTypeC.WebApp.Controllers
{
    public class BeerController : Controller
    {
        private readonly IBeerServices _beerServices;
        private readonly IMapper _mapper;
        private readonly IRepository _repository;
        public BeerController(IBeerServices beerServices, IMapper mapper, IRepository repository)
        {
            _beerServices= beerServices;
            _mapper= mapper;
            _repository= repository;
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
            return View(model);
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
                var a = _beerServices.AddFromView(beerFromView);
                _repository.Insert(a);
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
            return View();
        }

        // POST: BeerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BeerController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BeerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
