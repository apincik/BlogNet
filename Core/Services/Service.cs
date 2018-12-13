using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public abstract class Service<T> : IService<T> where T : BaseEntity
    {
        protected IAsyncModel<T> Repository;

        public Service(IAsyncModel<T> model)
        {
            Repository = model;
        }

        public virtual Task<int> Count()
        {
            return Repository.Table().CountAsync();
        }

        public virtual Task<T> Get(params object[] values)
        {
            return Repository.Table().FindAsync(values);
        }

        public virtual Task<List<T>> ListAll()
        {
            return Repository.Table().ToListAsync();
        }

        public virtual Task<List<T>> List(int limit, int offset)
        {
            return Repository.Table().AsQueryable().Take(limit).Skip(limit * offset).ToListAsync();
        }

        public virtual IQueryable<T> Query()
        {
            return (IQueryable<T>)Repository.Table().AsQueryable();
        }
    }
}
