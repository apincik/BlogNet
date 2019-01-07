using Core.Entities;
using Core.Interfaces;
using Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class ArticleRepository : Repository<Article>, IArticleRepository
    {
        public ArticleRepository(WebContext dbContext) : base(dbContext)
        {
        }

        public override Task<List<Article>> ListAll()
        {
            return _dbContext.Articles
                .Include(m => m.Category)
                .Include(m => m.Seo)
                .ToListAsync();
        }

        public Task<List<Article>> ListAllByProjectId(int projectId)
        {
            return _dbContext.Articles
                .Include(m => m.Category)
                .Include(m => m.Seo)
                .Where(m => m.ProjectId == projectId)
                .ToListAsync();
        }

        public override Task<Article> Get(int id)
        {
            return _dbContext.Articles
                .Include(a => a.Category)
                .Include(a => a.Seo)
                .Include(a => a.PhotoHeader)
                .Include(a => a.PhotoThumbnail)
                .Include(a => a.Album)
                    .ThenInclude(album => album.Photos)
                .Include(a => a.ArticleSettings)
                    .AsNoTracking()
                .Where(a => a.Id == id)
                .FirstAsync();
        }

        /// <summary>
        /// Example - redirect purpose.
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<string> GetProjectArticleSlugById(string projectDomain, int id)
        {
            var article = await _dbContext.Articles
                .AsNoTracking()
                .Include(a => a.Project)
                .Where(a => a.Project.DomainName == projectDomain)
                .Where(a => a.Id == id)
                .Select(a => new Article { Slug = a.Slug })
                .FirstOrDefaultAsync();

            return article?.Slug;
        }

        /// <summary>
        /// Get one article by projectId and slug with joined data.
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="slug"></param>
        /// <returns></returns>
        public Task<Article> GetProjectArticleBySlug(string projectDomain, string slug)
        {
            return _dbContext.Articles
                .AsNoTracking()
                .Include(a => a.Category)
                .Include(a => a.Seo)
                .Include(a => a.PhotoHeader)
                .Include(a => a.PhotoThumbnail)
                .Include(a => a.Album)
                    .ThenInclude(album => album.Photos)
                .Include(a => a.ArticleSettings)
                .Include(a => a.Project)
                .Where(a => a.Project.DomainName == projectDomain)
                .Where(a => a.Slug == slug)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Get list for project article by category title.
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="categoryTitle"></param>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public async Task<List<Article>> ListProjectArticlesByCategoryTitle(string projectDomain, string categoryTitle, int limit, int offset)
        {
            // @TODO Move to categoryRepository, inject.
            var category = await _dbContext.Categories.Include(c => c.SubCategories).Where(c => c.Title.ToLower() == categoryTitle.ToLower()).FirstOrDefaultAsync();
            List<string> categories = category != null ? category.SubCategoryTree : new List<string>();

            return await _dbContext.Articles
                .AsNoTracking()
                .Include(a => a.Category)
                .Include(a => a.PhotoThumbnail)
                .Include(a => a.Project)
                .Where(a => a.Project.DomainName == projectDomain)
                .Where(a => categories.Contains(a.Category.Title))
                .Take(limit)
                .Skip(limit * offset)
                .Select(a => new Article { Title = a.Title, Description = a.Description, PhotoThumbnail = a.PhotoThumbnail, Slug = a.Slug })
                .ToListAsync();
        }

        public async Task<int> CountProjectArticlesByCategoryTitle(string projectDomain, string categoryTitle)
        {
            var category = await _dbContext.Categories.Include(c => c.SubCategories).Where(c => c.Title.ToLower() == categoryTitle.ToLower()).FirstOrDefaultAsync();
            List<string> categories = category != null ? category.SubCategoryTree : new List<string>();

            return await _dbContext.Articles
                .Where(a =>a.Project.DomainName == projectDomain)
                .Where(a => categories.Contains(a.Category.Title))
                .CountAsync();
        }
    }
}
