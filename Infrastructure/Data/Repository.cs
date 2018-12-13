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
    public class Repository<T> : IAsyncModel<T> where T : BaseEntity
    {
        protected readonly WebContext _dbContext;
        //public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();

        public Repository(WebContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<T> AddAsync(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public Task UpdateAsync(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            return  _dbContext.SaveChangesAsync();
        }

        public Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            return _dbContext.SaveChangesAsync();
        }

        public DbSet<T> Table()
        {
            return _dbContext.Set<T>();
        }
    }
}
