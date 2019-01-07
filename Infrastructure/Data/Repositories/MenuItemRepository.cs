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
    public class MenuItemRepository : Repository<MenuItem>, IMenuItemRepository
    {
        public MenuItemRepository(WebContext dbContext) : base(dbContext)
        {
        }

        public Task<List<MenuItem>> ListAllByProjectId(int id)
        {
            return _dbContext.MenuItems
                .Where(m => m.ProjectId == id)
                .ToListAsync();
        }
    }
}
