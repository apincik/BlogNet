using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ICategoryService
    {
        Task Create(Category category);
        Task Update(Category category);
        Task ToggleStatusById(int id);
        
    }
}
