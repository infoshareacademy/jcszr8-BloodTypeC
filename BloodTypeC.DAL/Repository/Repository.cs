using BloodTypeC.DAL.Contexts;
using BloodTypeC.DAL.Models.BaseEntity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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

        public async Task<List<T>> GetAll(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _entities;
            List<T> result = new();
            if (includes.Any())
            {
                foreach (var include in includes)
                {
                    result.AddRange(await query.Include(include).Distinct().ToListAsync());
                    return result;
                }
            }

            return await _entities.ToListAsync();
        }
        public async Task Insert(T entity)
        {
            if (entity != null)
            {
                _entities.Add(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task Delete(T entity)
        {
            _entities.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {
            _entities.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<T> GetById(string id)
        {
            return await _entities.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<T> GetById(string id, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _entities;
            List<T> result = new();
            foreach (var include in includes)
            {
                result.AddRange(await query.Include(include).Distinct().ToListAsync());
            }
            return result.FirstOrDefault(x => x.Id == id);
        }
    }
}
