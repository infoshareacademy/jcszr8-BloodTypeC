using BloodTypeC.DAL.Models.BaseEntity;

namespace BloodTypeC.DAL.Repository
{
    public interface IRepository<T> where T : Entity
    {
        public List<T> GetAll();
        public void Insert(T entity);
        public void Delete(T entity);
        public void Update(T entity);
        public T GetById(string id);
    }
}
