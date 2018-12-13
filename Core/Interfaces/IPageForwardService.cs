using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IPageForwardService : IService<PageForward>
    {
        Task DeleteById(int id);
        Task<PageForward> Create(PageForward variable);
        Task<PageForward> Update(PageForward variable);
        Task<List<PageForward>> ListAllByProjectId(int id);
    }
}
