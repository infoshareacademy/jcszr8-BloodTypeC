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
        private const double MaxAlcoholValue = 94.99;
        private const double MaxScore = 10;

        public BeerServices(IRepository<Beer> repository)
        {
            _repository = repository;
        }
        public void AddFromView(BeerViewModel beerFromView)
        {
            var beerToAdd = new Beer();
            beerToAdd.Name = Format.AsNameOrTitle(beerFromView.Name, Format.CapitalsOptions.FirstWord, false);
            beerToAdd.Brewery = Format.AsNameOrTitle(beerFromView.Brewery, Format.CapitalsOptions.EachWord, false);
            beerToAdd.Style = Format.AsNameOrTitle(beerFromView.Style, Format.CapitalsOptions.EachWord, true);
            beerToAdd.Flavors = Format.AsTags(beerFromView.FlavorString);
            beerToAdd.AlcoholByVolume = Format.AsScoreOrABV(beerFromView.AlcoholByVolume.ToString(), MaxAlcoholValue);
            beerToAdd.Score = Format.AsScoreOrABV(beerFromView.Score.ToString(), MaxScore);
            beerToAdd.Added = DateTime.Now;
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
            beerToEdit.Name = Format.AsNameOrTitle(beerFromView.Name, Format.CapitalsOptions.FirstWord, false);
            beerToEdit.Brewery = Format.AsNameOrTitle(beerFromView.Brewery, Format.CapitalsOptions.EachWord, false);
            beerToEdit.Style = Format.AsNameOrTitle(beerFromView.Style, Format.CapitalsOptions.EachWord, true);
            beerToEdit.Flavors = Format.AsTags(beerFromView.FlavorString);
            beerToEdit.AlcoholByVolume = Format.AsScoreOrABV(beerFromView.AlcoholByVolume.ToString(), MaxAlcoholValue);
            beerToEdit.Score = Format.AsScoreOrABV(beerFromView.Score.ToString(), MaxScore);
            beerToEdit.Added = DateTime.Now;
            _repository.Update(beerToEdit);
        }

        public void Delete(int id)
        {
            _repository.Delete(GetById(id));
        }
    }

}
