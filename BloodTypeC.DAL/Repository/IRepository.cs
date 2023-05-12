using BloodTypeC.DAL.Models.BaseEntity;
using System.Linq.Expressions;

namespace BloodTypeC.DAL.Repository
{
    public interface IRepository<T> where T : Entity
    {
        public List<T> GetAll(Expression<Func<T, object>>? include = null);
        public void Insert(T entity);
        public void Delete(T entity);
        public void Update(T entity);
        public T GetById(string id);
    }
}
