using BloodTypeC.DAL.Models;

namespace BloodTypeC.Logic.Services.IServices
{
    public interface IFavoriteBeersServices
    {
        Task AddToFavs(string beerId, string userName);
        Task RemoveFromFavs(string beerId, string userName);
        Task<IEnumerable<Beer>> GetAllFavs(string userName);
    }
}
