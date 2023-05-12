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

        public FavoriteBeersServices(IRepository<Beer> beerRepository, UserManager<User> userManager)
        {
            _beerRepository = beerRepository;
            _userManager = userManager;
        }

        public void AddToFavs(string beerId, string userId)
        {
            var beer = _beerRepository.GetById(beerId);
            beer.FavoriteUsers.Add(_userManager.Users.FirstOrDefault(x => x.Id == userId));
            _beerRepository.Update(beer);
        }

        public IEnumerable<Beer> GetAllFavs(string userId)
        {
            var favoriteBeers = _beerRepository.GetAll().Where(beer => beer.FavoriteUsers.Any(user => user.Id == userId)); 
            return favoriteBeers;
        }

        public void RemoveFromFavs(string beerId, string userId)
        {
            
        }
    }
}
