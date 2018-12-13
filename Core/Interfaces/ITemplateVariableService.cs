using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ITemplateVariableService : IService<TemplateVariable>
    {
        Task ToggleStatusById(int id);
        Task<TemplateVariable> Create(TemplateVariable variable);
        Task<TemplateVariable> Update(TemplateVariable variable);
        Task<List<TemplateVariable>> ListAllByProjectId(int id);
    }
}
