using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly WebContext _dbContext;
        
        public Repository(WebContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<T> Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public Task Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            return  _dbContext.SaveChangesAsync();
        }

        public Task Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            return _dbContext.SaveChangesAsync();
        }

        public virtual Task<int> Count()
        {
            return _dbContext.Set<T>().CountAsync();
        }

        public virtual Task<T> Find(params object[] values)
        {
            return _dbContext.Set<T>().FindAsync(values);
        }

        public virtual Task<T> Get(int id)
        {
            return _dbContext.Set<T>().FindAsync(id);
        }

        public virtual Task<List<T>> ListAll()
        {
            return _dbContext.Set<T>().ToListAsync();
        }

        public virtual Task<List<T>> List(int limit, int offset)
        {
            return _dbContext.Set<T>()
                .Take(limit)
                .Skip(limit * offset)
                .ToListAsync();
        }

        public virtual IQueryable<T> Query()
        {
            return (IQueryable<T>)_dbContext.Set<T>().AsQueryable();
        }
    }
}
