using DataAccessLayer.EntityFrameworkCore;
using EntityLayer.Abstracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace DataAccessLayer.Abstracts
{
    public class GenericRepository<T> : IGenericRepository<T> where T : IEntity, new()
    {
        private readonly TraversalDbContext context;

        public GenericRepository(TraversalDbContext context)
        {
            this.context = context;
        }

        public DbSet<T> Table => context.Set<T>();

        public IQueryable<T> GetAll(bool tracking = false)
        {
            var query = Table.AsQueryable();
            if (!tracking)
            {
                query = query.AsNoTracking();
            }
            return query;
        }
        public IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool tracking = true)
        {
            var query = Table.Where(method);
            if (!tracking)
            {
                query = query.AsNoTracking();
            }
            return  query;
        }

        public async Task<T> GetByIdAsync(int id, bool tracking = true)
        {
            var query = Table.AsSingleQuery();
            if (!tracking)
            {
                query = query.AsNoTracking();
            }
            return await Table.FindAsync(id);
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = true)
        {
            var query = Table.AsQueryable();
            if (!tracking)
            {
                query = query.AsNoTracking();
            }
            return await query.FirstOrDefaultAsync(method);
        }
        public async Task<bool> AddAsync(T entity)
        {
            EntityEntry<T> entityEntry = await Table.AddAsync(entity);
            await SaveAsync();
            return entityEntry.State == EntityState.Added;
        }

        public async Task<bool> AddRangeAsync(List<T> entities)
        {
            await Table.AddRangeAsync(entities);
            await SaveAsync();
            return true;
        }

        public async Task<bool> Remove(T entity)
        {
            EntityEntry<T> entityEntry = Table.Remove(entity);
            await SaveAsync();
            return entityEntry.State == EntityState.Deleted;
        }

        public async Task<bool> RemoveRange(List<T> entities)
        {
            Table.RemoveRange(entities);
            await SaveAsync();
            return true;
        }

        public async Task<bool> RemoveAsync(T entity)
        {
            T model = await GetByIdAsync(entity.Id);
            return await Remove(model);
        }

        public async Task<bool> Update(T entity)
        {
            EntityEntry entityEntry = Table.Update(entity);
            await SaveAsync();
            return entityEntry.State == EntityState.Modified;
        }

        public async Task<int> SaveAsync() => await context.SaveChangesAsync();
    }
}
