using BloodTypeC.DAL.Models;

namespace BloodTypeC.Logic.Services.IServices
{
    public interface IFavoriteBeersServices
    {
        void AddToFavs(string beerId, string userId);
        void RemoveFromFavs(string beerId, string userId);
        IEnumerable<Beer> GetAllFavs(string userId);
    }
}
