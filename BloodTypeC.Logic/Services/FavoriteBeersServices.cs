using BloodTypeC.DAL.Models;
using BloodTypeC.DAL.Repository;
using BloodTypeC.Logic.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace BloodTypeC.Logic.Services
{
    public class FavoriteBeersServices : IFavoriteBeersServices
    {
        private IEnumerable<Beer> _favoriteBeers;
        private readonly IRepository<Beer> _beerRepository;
        private readonly UserManager<User> _userManager;
        private readonly User _user;

        public FavoriteBeersServices(IRepository<Beer> beerRepository, UserManager<User> userManager)
        {
            _beerRepository = beerRepository;
            _userManager = userManager;
            _user = _userManager.Users.FirstOrDefault(x => x.Id == "da1ae07a-873f-4f8e-b922-c9cf9935d059");
        }

        public void AddToFavs(string beerId, string userId)
        {
            var beer = _beerRepository.GetById(beerId);
            beer.FavoriteUsers.Add(_userManager.Users.FirstOrDefault(x => x.Id == userId));
            _beerRepository.Update(beer);
        }

        public IEnumerable<Beer> GetAllFavs(string userId)
        {
            var favoriteBeers = _beerRepository.GetAll(x=> x.FavoriteUsers).Where(x => x.FavoriteUsers.Contains(_user)); 
            return favoriteBeers.ToList();
        }

        public void RemoveFromFavs(string beerId, string userId)
        {
            var beer = _beerRepository.GetById(beerId);
            beer.FavoriteUsers.Remove(_userManager.Users.FirstOrDefault(y => y.Id == userId));
            _beerRepository.Update(beer);
        }
    }
}
