using BloodTypeC.DAL.Models.BaseEntity;
using System.Linq.Expressions;

namespace BloodTypeC.DAL.Repository
{
    public interface IRepository<T> where T : Entity
    {
        public Task<List<T>> GetAll(Expression<Func<T, object>>? include = null);
        public Task Insert(T entity);
        public Task Delete(T entity);
        public Task Update(T entity);
        public Task<T> GetById(string id);
        public Task<T> GetById(string id, params Expression<Func<T, object>>[] includes);
    }
}
