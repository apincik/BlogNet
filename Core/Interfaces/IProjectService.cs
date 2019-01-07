using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IProjectService
    {
        Task Create(Project project);
        Task Update(Project project);
    }
}
