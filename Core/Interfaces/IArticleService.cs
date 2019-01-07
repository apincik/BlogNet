using Core.Entities;
using Core.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IArticleService
    {
        Task Create(Article article);
        Task Create(Article article, ArticleImagesDto articleImages);
        Task Update(Article article);
        Task Update(Article article, ArticleImagesDto articleImages);
        Task ToggleStatusById(int id);
    }
}
