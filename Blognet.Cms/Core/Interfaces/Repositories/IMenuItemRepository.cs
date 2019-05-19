using Blognet.Cms.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blognet.Cms.Core.Interfaces.Repositories
{
    public interface IMenuItemRepository : IRepository<MenuItem>
    {
        Task<List<MenuItem>> ListAllByProjectId(int id);
        Task<List<MenuItem>> GetProjectMenu(string domainName);
    }
}
