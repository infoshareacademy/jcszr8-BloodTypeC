using BloodTypeC.DAL.Models;
using BloodTypeC.Logic;
using BloodTypeC.WebApp.Models;
using BloodTypeC.WebApp.Services.IServices;

namespace BloodTypeC.WebApp.Services
{
    public class BeerServices : IBeerServices
    {
        private List<Beer> _allBeers = DB.AllBeers;
        private const double MaxAlcoholValue = 94.99;
        private const double MaxScore = 10;
        public void Add(Beer beer)
        {
            var beerToAdd = new Beer();
            beerToAdd.Id = GetNewId();
            beerToAdd.Name = Format.AsNameOrTitle(beer.Name, Format.CapitalsOptions.FirstWord, false);
            beerToAdd.Brewery = Format.AsNameOrTitle(beer.Brewery, Format.CapitalsOptions.EachWord, false);
            beerToAdd.Style = Format.AsNameOrTitle(beer.Style, Format.CapitalsOptions.EachWord, true);
            if (beer.Flavors.Any())
            {
                beerToAdd.Flavors = Format.AsTags(beer.Flavors.Aggregate((a, b) => a + " " + b));
            }
            beerToAdd.AlcoholByVolume = Format.AsScoreOrABV(beer.AlcoholByVolume.ToString(), MaxAlcoholValue);
            beerToAdd.Score = Format.AsScoreOrABV(beer.Score.ToString(), MaxScore);
            beerToAdd.Added = DateTime.Now;
            _allBeers.Add(beerToAdd);
        }
        public Beer AddFromView(BeerViewModel beerFromView)
        {
            var beerToAdd = new Beer();
            beerToAdd.Id = GetNewId();
            beerToAdd.Name = Format.AsNameOrTitle(beerFromView.Name, Format.CapitalsOptions.FirstWord, false);
            beerToAdd.Brewery = Format.AsNameOrTitle(beerFromView.Brewery, Format.CapitalsOptions.EachWord, false);
            beerToAdd.Style = Format.AsNameOrTitle(beerFromView.Style, Format.CapitalsOptions.EachWord, true);
            beerToAdd.Flavors = Format.AsTags(beerFromView.FlavorString);
            beerToAdd.AlcoholByVolume = Format.AsScoreOrABV(beerFromView.AlcoholByVolume.ToString(), MaxAlcoholValue);
            beerToAdd.Score = Format.AsScoreOrABV(beerFromView.Score.ToString(), MaxScore);
            beerToAdd.Added = DateTime.Now;
            return beerToAdd;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Edit(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Beer> GetAll()
        {
            return _allBeers;
        }

        public Beer GetById(int id)
        {
            return _allBeers.FirstOrDefault(x => x.Id == id.ToString());
        }

        private string GetNewId()
        {
            return _allBeers.Max(x => int.Parse(x.Id) + 1).ToString();
        }
    }
}
