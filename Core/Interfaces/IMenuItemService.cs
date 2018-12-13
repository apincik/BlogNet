using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IMenuItemService : IService<MenuItem>
    {
        Task<MenuItem> Create(MenuItem album);
        Task<MenuItem> Update(MenuItem album);
        Task<List<MenuItem>> ListAllByProjectId(int id);
        Task DeleteAsyncById(int id);
    }
}
