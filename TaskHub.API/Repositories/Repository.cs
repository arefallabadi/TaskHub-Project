using Microsoft.EntityFrameworkCore;
using TaskHub.API.Data;
using TaskHub.API.ParentEntities;

namespace TaskHub.API.Repositories.Implementations
{
    public class Repository<T> : IRepository<T> where T : IdEntity
    {
        protected readonly TaskHubDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public Repository(TaskHubDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public List<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public IQueryable<T> GetAllAsQueryable()
        {
            return _dbSet.AsQueryable();
        }

        public T? GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
            _context.SaveChanges();
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
            _context.SaveChanges();
        }
    }
}
