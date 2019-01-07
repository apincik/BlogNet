using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ITemplateVariableService
    {
        Task ToggleStatusById(int id);
        Task Create(TemplateVariable variable);
        Task Update(TemplateVariable variable);
    }
}
