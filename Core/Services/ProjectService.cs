using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class ProjectService : Service<Project>, IProjectService
    {
        public ProjectService(IAsyncModel<Project> model) : base(model)
        {
        }

        public Task<Project> Create(Project project)
        {
            return Repository.AddAsync(project);
        }

        public async Task<Project> Update(Project project)
        {
            // Project settings created as entity is being attached, otherwise call service.
            await Repository.UpdateAsync(project);
            return project;
        }

        public Task<List<Project>> ListAllByUserId(string userId)
        {
            return Repository.Table().Where(p => p.UserId == userId).ToListAsync();
        }

        public override Task<Project> Get(params object[] values)
        {
            return Repository.Table()
                .Include(m => m.ProjectSettings)
                .FirstOrDefaultAsync(m => m.Id == (int) values[0]);
        }
    }
}
