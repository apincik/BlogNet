using Core.Entities;
using Core.Interfaces;
using Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class ProjectService : IProjectService
    {
        private IProjectRepository _projectRepository;

        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public Task Create(Project project)
        {
            return _projectRepository.Add(project);
        }

        public Task Update(Project project)
        {
            // Project settings created as entity is being attached, otherwise call service.
            return _projectRepository.Update(project);
        }
    }
}
