using BloodTypeC.DAL.Contexts;
using BloodTypeC.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace BloodTypeC.DAL.Repository
{
    public class BeerRepository : IRepository
    {
        private readonly DbSet<Beer> _beerEntities;
        private readonly BeeropediaContext _context;

        public BeerRepository(BeeropediaContext context)
        {
            _context = context;
            _beerEntities = context.Set<Beer>();
        }

        public List<Beer> GetAll()
        {
            return this._beerEntities.AsEnumerable().ToList();
        }
        public void Insert(Beer entity)
        {
            if (entity == null)
            {
                return; // throw exception - incorrect usage
            }

            this._beerEntities.Add(entity);
            this._context.SaveChanges();
        }

        public void Delete(Beer entity)
        {
            this._beerEntities.Remove(entity);
            this._context.SaveChanges();
        }

        public void Update(Beer entity)
        {
            this._beerEntities.Update(entity);
            this._context.SaveChanges();
        }

        public Beer GetById(string id)
        {
            return _beerEntities.FirstOrDefault(x => x.Id == id);
        }
    }
}
