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

        public Beer GetById(int id)
        {
            return _repository.GetById(id.ToString());
        }

        public void EditFromView(BeerViewModel beerFromView)
        {
            var beerToEdit = _repository.GetById(beerFromView.Id);
            beerToEdit.Name = Formatters.AsNameOrTitle(beerFromView.Name, Formatters.CapitalsOptions.FirstWord, false);
            beerToEdit.Brewery = Formatters.AsNameOrTitle(beerFromView.Brewery, Formatters.CapitalsOptions.EachWord, false);
            beerToEdit.Style = Formatters.AsNameOrTitle(beerFromView.Style, Formatters.CapitalsOptions.EachWord, true);
            beerToEdit.Flavors = Formatters.AsTags(beerFromView.FlavorString);
            beerToEdit.AlcoholByVolume = Formatters.AsScoreOrABV(beerFromView.AlcoholByVolume.ToString(), MaxAlcoholValue);
            beerToEdit.Score = Formatters.AsScoreOrABV(beerFromView.Score.ToString(), MaxScore);
            beerToEdit.Added = DateTime.Now;
            _repository.Update(beerToEdit);
        }

        public void Delete(int id)
        {
            _repository.Delete(GetById(id));
        }
    }

}
