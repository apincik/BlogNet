using Core.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> Add(T entity);
        Task Update(T entity);
        Task Delete(T entity);

        // @TODO Possibly remove, add specification like base methods.
        IQueryable<T> Query();
        Task<List<T>> ListAll();
        Task<T> Get(int id);
        Task<T> Find(params object[] values);
        Task<int> Count();
        Task<List<T>> List(int limit, int offset);
    }
}
