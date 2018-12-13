using Core.Entities;
using Core.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IArticleService : IService<Article>
    {
        Task<Article> Create(Article article);
        Task<Article> Create(Article article, ArticleImagesDto articleImages);
        Task<Article> Update(Article article);
        Task<Article> Update(Article article, ArticleImagesDto articleImages);

        Task<List<Article>> ListAllByProjectId(int projectId);
        Task ToggleStatusById(int id);
        Task<Article> Get(int id);
    }
}
