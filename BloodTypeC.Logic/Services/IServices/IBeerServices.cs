using BloodTypeC.DAL.Models;
using BloodTypeC.WebApp.Models;

namespace BloodTypeC.Logic.Services.IServices
{
    public interface IBeerServices
    {
        IEnumerable<Beer> GetAll();
        Beer GetById(int id);
        void Delete(int id);
        void AddFromView(BeerViewModel beerFromView);
        void EditFromView(BeerViewModel beerFromView);

    }
}