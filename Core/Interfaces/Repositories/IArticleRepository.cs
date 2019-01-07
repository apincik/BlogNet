using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Repositories
{
    public interface IArticleRepository : IRepository<Article>
    {
        Task<List<Article>> ListAll();
        Task<List<Article>> ListAllByProjectId(int projectId);
        Task<Article> Get(int id);

        Task<string> GetProjectArticleSlugById(string projectDomain, int id);
        Task<List<Article>> ListProjectArticlesByCategoryTitle(string projectDomain, string categoryTitle, int limit, int offset);
        Task<Article> GetProjectArticleBySlug(string projectDomain, string slug);
        Task<int> CountProjectArticlesByCategoryTitle(string projectDomain, string categoryTitle);
    }
}
