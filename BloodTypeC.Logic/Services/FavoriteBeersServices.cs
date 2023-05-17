using BloodTypeC.DAL.Models;
using BloodTypeC.DAL.Models.Views;
using BloodTypeC.DAL.Repository;
using BloodTypeC.Logic.Services.IServices;
using BloodTypeC.WebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BloodTypeC.Logic.Services
{
    public class FavoriteBeersServices : IFavoriteBeersServices
    {
        private readonly IRepository<Beer> _beerRepository;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;

        public FavoriteBeersServices(IRepository<Beer> beerRepository, UserManager<User> userManager)
        {
            _beerRepository = beerRepository;
            _userManager = userManager;
        }

        public async Task AddToFavs(string beerId, string userName)
        {
            var beer = await _beerRepository.GetById(beerId);
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == userName);
            beer.FavoriteUsers.Add(user);
            await _beerRepository.Update(beer);
        }

        public async Task<IEnumerable<Beer>> GetAllFavs(string userName)
        {
            //var user = await _userManager.FindByEmailAsync(userName);
            var user = await _userManager.Users.Include(x => x.FavoriteBeers).SingleOrDefaultAsync(u => u.UserName == userName);
            var favoriteBeers = user.FavoriteBeers;
            return favoriteBeers;
        }

        public async Task RemoveFromFavs(string beerId, string userName)
        {
            var beer = await _beerRepository.GetById(beerId);
            var user = await _userManager.Users.Include(x => x.FavoriteBeers).SingleOrDefaultAsync(u => u.UserName == userName);
            beer.FavoriteUsers.Remove(user);
            await _beerRepository.Update(beer);
        }
    }
}
