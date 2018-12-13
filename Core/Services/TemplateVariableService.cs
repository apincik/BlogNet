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
    public class TemplateVariableService : Service<TemplateVariable>, ITemplateVariableService
    {
        public TemplateVariableService(IAsyncModel<TemplateVariable> model) : base(model)
        {

        }

        public Task<TemplateVariable> Create(TemplateVariable variable)
        {
            return Repository.AddAsync(variable);
        }

        public async Task<TemplateVariable> Update(TemplateVariable variable)
        {
            await Repository.UpdateAsync(variable);
            return variable;
        }


        public Task<List<TemplateVariable>> ListAllByProjectId(int id)
        {
            return Repository.Table()
                .Include(m => m.ParentTemplateVariable)
                .Where(m => m.ProjectId == id)
                .ToListAsync();
        }

        public async Task ToggleStatusById(int id)
        {
            TemplateVariable variable = await Repository.Table().FindAsync(id);
            variable.Status = variable.Status == Status.Inactive ? Status.Active : Status.Inactive;
            await Repository.UpdateAsync(variable);
        }
    }
}
