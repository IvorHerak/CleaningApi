using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CleaningManagement.DAL.Entities.Repository
{
    public interface IRepository<T> where T : class
    {
        public Task<IEnumerable<T>> GetAsync
        (
            Expression<Func<T, bool>> filter = null,
            bool disableTracking = false
        );

        public Task<T> GetOneAsync
        (
            Expression<Func<T, bool>> filter = null,
            bool disableTracking = false
        );

        public void Add(T entity);

        public void Update(T entity);

        public void Delete(T entity);

        Task<int> SaveAsync();
    }
}
