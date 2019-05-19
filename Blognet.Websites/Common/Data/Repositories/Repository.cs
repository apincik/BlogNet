using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blognet.Websites.Commons.Data.Repositories
{
    public class Repository : IRepository
    {
        private IClient _client;
        private string _domainName;

        public Repository(Client client, string domainName)
        {
            _client = client;
            _domainName = domainName;
        }

        public async Task<WebCategoryArticlesModel> FindCategoryArticlesAsync(string title, int limit, int page)
        {
            try { 
                return await _client.FindCategoryArticlesAsync(title, limit, page, _domainName);
            }
            // @TODO add custom exceptions
            catch (Exception) 
            {
                return new WebCategoryArticlesModel
                {
                    Articles = new List<ArticleListItem>(),
                    ArticlesCount = 0,
                    Limit = 0,
                    Page = 1
                };
            }
}

        public async Task<WebArticleModel> GetArticleBySlugAsync(string slug)
        {
            try
            {
                return await _client.GetArticleBySlugAsync(slug, _domainName);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<WebArticleSlugModel> GetArticleSlugByIdAsync(int id)
        {
            try
            {
                 return await _client.GetArticleSlugByIdAsync(id, _domainName);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<WebMenuModel> GetMenuAsync()
        {
            try
            {
                return await _client.GetMenuAsync(_domainName);
            }
            catch (Exception)
            {
                return new WebMenuModel
                {
                    MenuItems = new List<MenuItemDTO>(),
                };
            }
        }

        public async Task<WebArticleSlugModel> GetForwardedArticleByIdAndSlug(int id, string slug)
        {
            try
            {
                string mask = Uri.EscapeUriString($"/{id}/{slug}/");
                return await _client.GetForwardedArticleByMaskAsync(mask, _domainName);
            }
            catch(Exception)
            {
                return null;
            }
        }

    }
}
