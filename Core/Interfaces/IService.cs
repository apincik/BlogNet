using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IService<T> where T : BaseEntity
    {
        IQueryable<T> Query();
        Task<List<T>> ListAll();
        Task<T> Get(params object[] values);
        Task<int> Count();
        Task<List<T>> List(int limit, int offset);
    }
}
