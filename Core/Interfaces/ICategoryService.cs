using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ICategoryService : IService<Category>
    {
        Task Create(Category category);
        Task Update(Category category);
        Task<List<Category>> ListAllWithParentCategory();
        Task<List<Category>> ListAllByProjectId(int projectId);
        Task ToggleStatusById(int id);
        Task<Category> GetWithSeo(int id);
    }
}
