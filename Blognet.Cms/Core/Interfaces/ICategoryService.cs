using Blognet.Cms.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blognet.Cms.Core.Interfaces
{
    public interface ICategoryService
    {
        Task Create(Category category);
        Task Update(Category category);
        Task ToggleStatusById(int id);
        
    }
}
