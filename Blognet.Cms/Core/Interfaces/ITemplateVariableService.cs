using Blognet.Cms.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blognet.Cms.Core.Interfaces
{
    public interface ITemplateVariableService
    {
        Task ToggleStatusById(int id);
        Task Create(TemplateVariable variable);
        Task Update(TemplateVariable variable);
    }
}
