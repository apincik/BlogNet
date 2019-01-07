using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Repositories
{
    public interface IPageForwardRepository : IRepository<PageForward>
    {
        Task<List<PageForward>> ListAllByProjectId(int id);
    }
}
