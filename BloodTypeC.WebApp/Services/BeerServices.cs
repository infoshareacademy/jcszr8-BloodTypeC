using BloodTypeC.DAL;
using BloodTypeC.Logic;
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
            beer.Id = _allBeers.Max(x => int.Parse(x.Id)+1).ToString();
            beer.Name = Format.AsNameOrTitle(beer.Name, Format.CapitalsOptions.FirstWord, false);
            beer.Brewery = Format.AsNameOrTitle(beer.Brewery, Format.CapitalsOptions.EachWord, false);
            beer.Style = Format.AsNameOrTitle(beer.Style, Format.CapitalsOptions.EachWord, true);
            if (beer.Flavors.Any())
            {
                beer.Flavors = Format.AsTags(beer.Flavors.Aggregate((a, b) => a + " " + b));
            }
            beer.AlcoholByVolume = Format.AsScoreOrABV(beer.AlcoholByVolume.ToString(), MaxAlcoholValue);
            beer.Score = Format.AsScoreOrABV(beer.Score.ToString(), MaxScore);
            beer.Added = DateTime.Now;
            _allBeers.Add(beer);
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

        public void Edit(IFormCollection collection)
        {
            throw new NotImplementedException();
        }

        public void Edit()
        {
            throw new NotImplementedException();
        }
    }
}
