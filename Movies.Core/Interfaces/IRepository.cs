using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Core.Interfaces
{
    public interface IRepository
    {
        Task<List<T>> GetAllAsync<T>() where T : class;
        Task<List<T>> GetAllAsync<T>(params Expression<Func<T, object>>[] includes) where T : class;
        Task<List<T>> GetAllAsync<T>(Expression<Func<T, bool>> predicate) where T : class;
        Task<List<T>> GetAllAsync<T>(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes) where T : class;
        ValueTask<T> GetByIdAsync<T>(int id) where T : class;
        Task AddAsync<T>(T entity) where T : class;
        Task UpdateAsync<T>(T entity) where T : class;
        Task DeleteAsync<T>(T entity) where T : class;
        Task DeleteAsync<T>(List<T> entities) where T : class;
    }
}
