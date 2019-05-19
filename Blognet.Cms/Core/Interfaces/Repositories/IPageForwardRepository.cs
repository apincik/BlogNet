using Blognet.Cms.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blognet.Cms.Core.Interfaces.Repositories
{
    public interface IPageForwardRepository : IRepository<PageForward>
    {
        Task<List<PageForward>> ListAllByProjectId(int id);

        Task<PageForward> GetProjectArticlePageForwardByMask(string projectDomain, string mask);
    }
}
