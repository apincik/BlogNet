using Blognet.Cms.Core.Model;
using Blognet.Cms.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blognet.Cms.Core.Interfaces
{
    public interface IArticleAlbumService
    {
        Task<int> CreateAlbum(Article article);
        Task<Article> SaveArticleImages(Article storedArticle, ArticleImagesDTO articleImages);
    }
}
