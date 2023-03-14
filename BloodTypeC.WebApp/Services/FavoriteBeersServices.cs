using BloodTypeC.DAL;
using BloodTypeC.WebApp.Services.IServices;

namespace BloodTypeC.WebApp.Services
{
    public class FavoriteBeersServices : IFavoriteBeersServices
    {
        private readonly List<Beer> _allBeers = DB.AllBeers;
        private readonly List<Beer> _favoriteBeers = DB.FavoriteBeers;
        public void AddToFavs(int id)
        {
            var beer = _allBeers.FirstOrDefault(x => x.Id == id.ToString());
            if (!_favoriteBeers.Contains(beer))
            {
                _favoriteBeers?.Add(beer);
            }
        }

        public List<Beer> GetAllBeers()
        {
            return _allBeers;
        }

        public List<Beer> GetAllFavs()
        {
            return _favoriteBeers;
        }

        public void RemoveFromFavs(int id)
        {
            var beer = _allBeers.FirstOrDefault(x => x.Id == id.ToString());
            _favoriteBeers?.Remove(beer);
        }
    }
}
