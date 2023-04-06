using BloodTypeC.DAL.Contexts;
using BloodTypeC.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace BloodTypeC.DAL.Repository
{
    public class Repository : IRepository
    {
        private readonly DbSet<Beer> _entities;
        private readonly BeeropediaContext _context;

        public Repository(BeeropediaContext context)
        {
            _context = context;
            _entities = context.Set<Beer>();
        }

        public List<Beer> GetAll()
        {
            return this._entities.AsEnumerable().ToList();
        }
        public void Insert(Beer entity)
        {
            if (entity == null)
            {
                return; // throw exception - incorrect usage
            }

            this._entities.Add(entity);
            this._context.SaveChanges();
        }

        public void Delete(Beer entity)
        {
            throw new NotImplementedException();
        }

        Beer Get(string id)
        {
            throw new NotImplementedException();
        }

        public void Update(Beer entity)
        {
            throw new NotImplementedException();
        }

        Beer IRepository.Get(string id)
        {
            throw new NotImplementedException();
        }
    }
}
