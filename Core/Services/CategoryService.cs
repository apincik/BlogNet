using Core.Entities;
using Core.Enum;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class CategoryService : Service<Category>, ICategoryService
    {
        ISeoService _seoService;

        public CategoryService(IAsyncModel<Category> model, ISeoService seoService) : base(model)
        {
            _seoService = seoService;
        }

        public async Task ToggleStatusById(int id)
        {
            Category cat = await Repository.Table().FindAsync(id);
            cat.Status = cat.Status == Status.Inactive ? Status.Active : Status.Inactive;
            await Repository.UpdateAsync(cat);
        }

        public async Task Create(Category category)
        {
            if (category.Seo.IsEmpty())
            {
                category.Seo = null;
                category.SeoId = null;
            }

            await Repository.AddAsync(category);
        }

        public async Task Update(Category category)
        {
            if (category.Seo.IsEmpty() && category.Seo.Id == 0)
            {
                category.Seo = null;
                category.SeoId = null;
            }

            await Repository.UpdateAsync(category);
        }

        #region Listing

        public override Task<List<Category>> ListAll()
        {
            return Repository.Table()
                .Include(m => m.ParentCategory)
                .Include(m => m.Seo)
                .ToListAsync();
        }

        public Task<List<Category>> ListAllWithParentCategory()
        {
            return Repository.Table()
                .Include(m => m.ParentCategory)
                .ToListAsync();
        }

        public Task<List<Category>> ListAllByProjectId(int projectId)
        {
            return Repository.Table()
                .Include(m => m.ParentCategory)
                .Include(m => m.Seo)
                .Where(m => m.ProjectId == projectId)
                .ToListAsync();
        }

        public Task<Category> GetWithSeo(int id)
        {
            return Repository.Table()
                .Include(c => c.Seo)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        #endregion
    }
}
