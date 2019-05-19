using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blognet.Websites.Commons.Data.Repositories
{
    public interface IRepository
    {
        Task<WebCategoryArticlesModel> FindCategoryArticlesAsync(string title, int limit, int page);
        Task<WebArticleModel> GetArticleBySlugAsync(string slug);
        Task<WebArticleSlugModel> GetArticleSlugByIdAsync(int id);
        Task<WebMenuModel> GetMenuAsync();
        Task<WebArticleSlugModel> GetForwardedArticleByIdAndSlug(int id, string slug);
    }
}
