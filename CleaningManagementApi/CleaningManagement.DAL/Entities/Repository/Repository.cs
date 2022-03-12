using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CleaningManagement.DAL.Entities.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly CleaningManagementDbContext context;

        public Repository(CleaningManagementDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<T>> GetAsync
        (
            Expression<Func<T, bool>> filter = null,
            bool disableTracking = false
        )
        {
            var query = GetQueryable(filter, disableTracking);
            return await query.ToListAsync();
        }

        public async Task<T> GetOneAsync
        (
            Expression<Func<T, bool>> filter = null,
            bool disableTracking = false
        )
        {
            var query = GetQueryable(filter, disableTracking);
            return await query.SingleOrDefaultAsync();
        }

        public void Add(T entity)
        {
            context.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            context.Set<T>().Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            var dbSet = context.Set<T>();
            if (context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity);
        }

        public Task<int> SaveAsync()
        {
            return context.SaveChangesAsync();
        }

        public IQueryable<T> GetQueryable(Expression<Func<T, bool>> filter, bool disableTracking)
        {
            IQueryable<T> query = context.Set<T>();

            query = ProcessFilter(filter, query);
            query = ProcessDisableTracking(disableTracking, query);

            return query;
        }

        protected static IQueryable<T> ProcessFilter(Expression<Func<T, bool>> filter, IQueryable<T> query)
        {
            if (filter == null)
            {
                return query;
            }

            query = query.Where(filter);
            return query;
        }

        protected static IQueryable<TEntity> ProcessDisableTracking<TEntity>(bool disableTracking, IQueryable<TEntity> query) where TEntity : class
        {
            if (disableTracking)
            {
                return query.AsNoTracking();
            }
            else
            {
                return query;
            }
        }
    }
}
