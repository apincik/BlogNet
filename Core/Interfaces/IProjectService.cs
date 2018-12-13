using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IProjectService : IService<Project>
    {
        Task<Project> Create(Project project);
        Task<Project> Update(Project project);
        Task<List<Project>> ListAllByUserId(string userId);
    }
}
