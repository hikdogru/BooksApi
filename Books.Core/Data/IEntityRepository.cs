using Books.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Books.Core.Data
{
    public interface IEntityRepository<T> where T: class, IEntity, new()
    {
        // Synchronous methods
        T Get(Expression<Func<T, bool>> filter = null);
        List<T> GetAll(Expression<Func<T, bool>> filter = null);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);

        // Asynchronous methods
        Task<T> GetAsync(Expression<Func<T, bool>> filter = null);
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter = null);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
