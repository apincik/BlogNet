using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IMenuItemService
    {
        Task Create(MenuItem album);
        Task Update(MenuItem album);
        Task Delete(int id);
    }
}
