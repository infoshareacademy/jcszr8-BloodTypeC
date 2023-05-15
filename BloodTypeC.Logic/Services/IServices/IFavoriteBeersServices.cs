using BloodTypeC.DAL.Models;
using BloodTypeC.WebApp.Models;

namespace BloodTypeC.Logic.Services.IServices
{
    public interface IFavoriteBeersServices
    {
        Task AddToFavs(string beerId, string userName);
        Task RemoveFromFavs(string beerId, string userName);
        Task<IEnumerable<Beer>> GetAllFavs(string userName);
    }
}
