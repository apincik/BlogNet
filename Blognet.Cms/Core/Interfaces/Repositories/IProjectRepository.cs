using Blognet.Cms.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blognet.Cms.Core.Interfaces.Repositories
{
    public interface IProjectRepository : IRepository<Project>
    {
        Task<List<Project>> ListAllByUserId(string userId);
    }
}
