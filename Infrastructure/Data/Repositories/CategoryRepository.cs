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
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(WebContext dbContext) : base(dbContext)
        {
        }

        public override Task<List<Category>> ListAll()
        {
            return _dbContext.Categories
                .Include(m => m.ParentCategory)
                .Include(m => m.Seo)
                .ToListAsync();
        }

        public Task<List<Category>> ListAllWithParentCategory()
        {
            return _dbContext.Categories
                .Include(m => m.ParentCategory)
                .ToListAsync();
        }

        public Task<List<Category>> ListAllByProjectId(int projectId)
        {
            return _dbContext.Categories
                .Include(m => m.ParentCategory)
                .Include(m => m.Seo)
                .Where(m => m.ProjectId == projectId)
                .ToListAsync();
        }

        public Task<Category> GetWithSeo(int id)
        {
            return _dbContext.Categories
                .Include(c => c.Seo)
                .FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}
