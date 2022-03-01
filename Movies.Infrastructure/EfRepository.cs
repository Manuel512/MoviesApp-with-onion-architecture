using Microsoft.EntityFrameworkCore;
using Movies.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Infrastructure
{
    public class EfRepository : IRepository
    {
        private readonly AppDbContext _context;
        public EfRepository(AppDbContext context)
        {
            _context = context;
        }

        public Task AddAsync<T>(T entity) where T : class
        {
            _context.Add(entity);
            return _context.SaveChangesAsync();
        }

        public Task DeleteAsync<T>(T entity) where T : class
        {
            _context.Remove(entity);
            return _context.SaveChangesAsync();
        }

        public Task DeleteAsync<T>(List<T> entities) where T : class
        {
            _context.RemoveRange(entities);
            return _context.SaveChangesAsync();
        }

        public Task<List<T>> GetAllAsync<T>() where T : class
        {
            return _context.Set<T>().ToListAsync();
        }

        public Task<List<T>> GetAllAsync<T>(params Expression<Func<T, object>>[] includes) where T : class
        {
            var query = _context.Set<T>().AsQueryable();
            
            foreach (var include in includes)
            {
                var memberExpression = include.Body as MemberExpression;

                if (memberExpression != null)
                    query = query.Include(memberExpression.Member.Name);
            }
            return query.ToListAsync();
        }

        public Task<List<T>> GetAllAsync<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return _context.Set<T>().Where(predicate).ToListAsync();
        }

        public Task<List<T>> GetAllAsync<T>(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes) where T : class
        {
            var query = _context.Set<T>().Where(predicate);
            foreach (var include in includes)
            {
                var memberExpression = include.Body as MemberExpression;

                if (memberExpression != null)
                    query = query.Include(memberExpression.Member.Name);
            }
            return query.ToListAsync();
        }

        public ValueTask<T> GetByIdAsync<T>(int id) where T : class
        {
            return _context.Set<T>().FindAsync(id);
        }

        public Task UpdateAsync<T>(T entity) where T : class
        {
            _context.Entry(entity).State = EntityState.Modified;
            return _context.SaveChangesAsync();
        }
    }
}
