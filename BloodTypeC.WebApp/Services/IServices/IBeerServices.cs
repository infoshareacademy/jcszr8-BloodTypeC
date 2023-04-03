using BloodTypeC.DAL.Models;
using BloodTypeC.WebApp.Models;

namespace BloodTypeC.WebApp.Services.IServices
{
    public interface IBeerServices
    {
        IEnumerable<Beer> GetAll();
        Beer GetById(int id);
        void Add(Beer beer);
        void Delete(int id);
        void Edit(int id);
        public void AddFromView(BeerViewModel beerFromView);

    }
}