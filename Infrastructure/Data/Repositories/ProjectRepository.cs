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
    public class ProjectRepository : Repository<Project>, IProjectRepository
    {
        public ProjectRepository(WebContext dbContext) : base(dbContext)
        {
        }

        public Task<List<Project>> ListAllByUserId(string userId)
        {
            return _dbContext.Projects
                .Where(p => p.UserId == userId)
                .ToListAsync();
        }

        public override Task<Project> Get(int id)
        {
            return _dbContext.Projects
                .Include(m => m.ProjectSettings)
                .FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}
