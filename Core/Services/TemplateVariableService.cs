using Core.Entities;
using Core.Enum;
using Core.Interfaces;
using Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class TemplateVariableService : ITemplateVariableService
    {
        private ITemplateVariableRepository _templateVariableRepository;

        public TemplateVariableService(ITemplateVariableRepository templateVariableRepository)
        {
            _templateVariableRepository = templateVariableRepository;
        }

        public Task Create(TemplateVariable variable)
        {
            return _templateVariableRepository.Add(variable);
        }

        public Task Update(TemplateVariable variable)
        {
            return _templateVariableRepository.Update(variable);
        }

        public async Task ToggleStatusById(int id)
        {
            TemplateVariable variable = await _templateVariableRepository.Find(id);
            variable.Status = variable.Status == Status.Inactive ? Status.Active : Status.Inactive;
            await _templateVariableRepository.Update(variable);
        }
    }
}
