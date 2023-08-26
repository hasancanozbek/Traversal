using EntityLayer.Abstracts;
using System.Linq.Expressions;

namespace DataAccessLayer.Abstracts
{
    public interface IGenericRepository<T> where T : IEntity, new()
    {
        IQueryable<T> GetAll(bool tracking = false);
        IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool tracking = true);
        Task<T> GetByIdAsync(int id, bool tracking = true);
        Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = true);
        Task<bool> AddAsync(T entity);
        Task<bool> AddRangeAsync(List<T> entities);
        bool Remove(T entity);
        bool RemoveRange(List<T> entities);
        Task<bool> RemoveAsync(T entity);
        bool Update(T entity);
        Task<int> SaveAsync();
    }
}