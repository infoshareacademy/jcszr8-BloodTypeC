using BloodTypeC.DAL.Models;
using BloodTypeC.DAL.Repository;
using BloodTypeC.Logic.Services.IServices;
using System.Security.Cryptography.X509Certificates;

namespace BloodTypeC.Logic.Services
{
    public class FavoriteBeersServices : IFavoriteBeersServices
    {
        private readonly List<Beer> _allBeers;
        private List<Beer> _favoriteBeers = DB.FavoriteBeers;
        private readonly IRepository<Beer> _beerRepository;

        public FavoriteBeersServices(IRepository<Beer> beerRepository)
        {
            _beerRepository = beerRepository;
            _allBeers = _beerRepository.GetAll();
        }

        public void AddToFavs(string id)
        {
            var beer = _allBeers.FirstOrDefault(x => x.Id == id);
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

        public void RemoveFromFavs(string id)
        {
            var beer = _favoriteBeers.FirstOrDefault(x => x.Id == id);
            _favoriteBeers?.Remove(beer);
        }
    }
}
