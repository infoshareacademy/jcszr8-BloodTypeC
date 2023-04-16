using BloodTypeC.DAL.Contexts;
using BloodTypeC.DAL.Models;
using BloodTypeC.DAL.Models.BaseEntity;
using Microsoft.EntityFrameworkCore;

namespace BloodTypeC.DAL.Repository
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        private readonly DbSet<T> _entities;
        private readonly BeeropediaContext _context;

        public Repository(BeeropediaContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        public List<T> GetAll()
        {
            return _entities.AsEnumerable().ToList();
        }
        public void Insert(T entity)
        {
            if (entity == null)
            {
                return; // throw exception - incorrect usage
            }

            this._entities.Add(entity);
            this._context.SaveChanges();
        }

        public void Delete(T entity)
        {
            this._entities.Remove(entity);
            this._context.SaveChanges();
        }

        public void Update(T entity)
        {
            this._entities.Update(entity);
            this._context.SaveChanges();
        }

        public T GetById(string id)
        {
            return _entities.FirstOrDefault(x => x.Id == id);
        }
    }
}
