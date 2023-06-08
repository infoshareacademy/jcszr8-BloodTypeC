using BloodTypeC.DAL.Models;
using BloodTypeC.WebApp.Models;

namespace BloodTypeC.Logic.Services.IServices
{
    public interface IBeerServices
    {
        Task<IEnumerable<Beer>> GetAll();
        Task<Beer> GetById(string id);
        Task Delete(string id);
        Task AddFromView(BeerViewModel beerFromView, string userName);
        Task EditFromView(BeerViewModel beerFromView);

    }
}