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
    public class TemplateVariableRepository : Repository<TemplateVariable>, ITemplateVariableRepository
    {
        public TemplateVariableRepository(WebContext dbContext) : base(dbContext)
        {
        }

        public Task<List<TemplateVariable>> ListAllByProjectId(int id)
        {
            return _dbContext.TemplateVariables
                .Include(m => m.ParentTemplateVariable)
                .Where(m => m.ProjectId == id)
                .ToListAsync();
        }
    }
}
