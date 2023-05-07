using BloodTypeC.DAL.Models;
using BloodTypeC.WebApp.Models;

namespace BloodTypeC.Logic.Services.IServices
{
    public interface IBeerServices
    {
        IEnumerable<Beer> GetAll();
        Beer GetById(string id);
        void Delete(string id);
        void AddFromView(BeerViewModel beerFromView);
        void EditFromView(BeerViewModel beerFromView);

    }
}