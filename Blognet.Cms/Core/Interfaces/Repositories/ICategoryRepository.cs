using Blognet.Cms.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blognet.Cms.Core.Interfaces.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<List<Category>> ListAllWithParentCategory();
        Task<List<Category>> ListAllByProjectId(int projectId);
        Task<Category> GetWithSeo(int id);
    }
}
