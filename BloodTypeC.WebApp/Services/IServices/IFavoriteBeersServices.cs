using BloodTypeC.DAL;

namespace BloodTypeC.WebApp.Services.IServices
{
    public interface IFavoriteBeersServices
    {
        void AddToFavs(int id);
        void RemoveFromFavs(int id);
        List<Beer> GetAllBeers();
        List<Beer> GetAllFavs();
    }
}
