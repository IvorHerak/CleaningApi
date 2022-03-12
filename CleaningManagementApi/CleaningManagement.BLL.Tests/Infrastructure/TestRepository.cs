using CleaningManagement.DAL.Entities.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CleaningManagement.BLL.Tests.Infrastructure
{
    public class TestRepository<T> : IRepository<T> where T : class
    {
        public List<T> Data { get; }

        public TestRepository()
        {
            Data = new List<T>();
        }

        public void Add(T entity)
        {
            Data.Add(entity);
        }

        public void Update(T entity)
        {
        }

        public void Delete(T entity)
        {
            Data.Remove(entity);
        }

        public Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> filter = null, bool disableTracking = false)
        {
            var result = Data.AsQueryable().Where(filter).AsEnumerable();
            return Task.FromResult(result);
        }

        public Task<T> GetOneAsync(Expression<Func<T, bool>> filter = null, bool disableTracking = false)
        {
            var result = Data.AsQueryable().SingleOrDefault(filter);
            return Task.FromResult(result);
        }

        public Task<int> SaveAsync()
        {
            return Task.FromResult(1);
        }
    }
}
