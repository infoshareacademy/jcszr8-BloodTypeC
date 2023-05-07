using AutoMapper;
using BloodTypeC.DAL.Models;
using BloodTypeC.DAL.Repository;
using BloodTypeC.Logic;
using BloodTypeC.Logic.Services.IServices;
using BloodTypeC.WebApp.Models;

namespace BloodTypeC.Logic.Services
{
    public class BeerServices : IBeerServices
    {
        private readonly IRepository<Beer> _repository;
        private readonly IMapper _mapper;
        private const double MaxAlcoholValue = 95;
        private const double MaxScore = 10;

        public BeerServices(IRepository<Beer> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public void AddFromView(BeerViewModel beerFromView)
        {
            var beerToAdd = _mapper.Map<Beer>(beerFromView);
            _repository.Insert(beerToAdd);
        }

        public IEnumerable<Beer> GetAll()
        {
            return _repository.GetAll();
        }

        public Beer GetById(string id)
        {
            return _repository.GetById(id);
        }

        public void EditFromView(BeerViewModel beerFromView)
        {
            var beerToEdit = _mapper.Map<BeerViewModel, Beer>(beerFromView, _repository.GetById(beerFromView.Id));
            _repository.Update(beerToEdit);
        }

        public void Delete(string id)
        {
            _repository.Delete(GetById(id));
        }
    }

}
