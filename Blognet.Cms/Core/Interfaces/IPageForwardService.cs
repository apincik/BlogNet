using Blognet.Cms.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blognet.Cms.Core.Interfaces
{
    public interface IPageForwardService
    {
        Task DeleteById(int id);
        Task Create(PageForward variable);
        Task Update(PageForward variable);
    }
}
