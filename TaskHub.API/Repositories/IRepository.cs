using TaskHub.API.ParentEntities;

namespace TaskHub.API.Repositories
{
    public interface IRepository <T> where T : IdEntity
    {
        List<T> GetAll();
        T? GetById(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
