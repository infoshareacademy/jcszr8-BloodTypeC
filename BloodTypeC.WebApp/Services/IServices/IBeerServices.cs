using BloodTypeC.DAL;

namespace BloodTypeC.WebApp.Services.IServices
{
    public interface IBeerServices
    {
        List<Beer> GetAll();
        Beer GetById(string id);
        void Add(Beer beer);
        void Delete(string id);
        void Edit(string id);
    }
}