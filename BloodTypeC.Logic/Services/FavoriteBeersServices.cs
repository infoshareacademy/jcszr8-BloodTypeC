using BloodTypeC.DAL.Models;
using BloodTypeC.DAL.Repository;
using BloodTypeC.Logic.Services.IServices;

namespace BloodTypeC.Logic.Services
{
    public class FavoriteBeersServices : IFavoriteBeersServices
    {
        private readonly List<Beer> _allBeers;
        private readonly List<Beer> _favoriteBeers = DB.FavoriteBeers;
        private readonly IRepository<Beer> _beerRepository;

        public FavoriteBeersServices(IRepository<Beer> beerRepository)
        {
            _beerRepository = beerRepository;
            _allBeers = _beerRepository.GetAll();
        }

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
