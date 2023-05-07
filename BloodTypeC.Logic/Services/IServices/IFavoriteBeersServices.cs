using BloodTypeC.DAL.Models;

namespace BloodTypeC.Logic.Services.IServices
{
    public interface IFavoriteBeersServices
    {
        void AddToFavs(string id);
        void RemoveFromFavs(string id);
        List<Beer> GetAllBeers();
        List<Beer> GetAllFavs();
    }
}
