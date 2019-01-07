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
    public class PageForwardRepository : Repository<PageForward>, IPageForwardRepository
    {
        public PageForwardRepository(WebContext dbContext) : base(dbContext)
        {
        }

        public Task<List<PageForward>> ListAllByProjectId(int id)
        {
            return _dbContext.PageForwards
                .Where(m => m.ProjectId == id)
                .ToListAsync();
        }
    }
}
