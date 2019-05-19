using Blognet.Cms.Domain.Entities;
using Blognet.Cms.Core.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blognet.Cms.Core.Interfaces
{
    public interface IArticleService
    {
        Task Create(Article article);
        Task Create(Article article, ArticleImagesDTO articleImages);
        Task Update(Article article);
        Task Update(Article article, ArticleImagesDTO articleImages);
        Task ToggleStatusById(int id);
    }
}
