using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IPageForwardService
    {
        Task DeleteById(int id);
        Task Create(PageForward variable);
        Task Update(PageForward variable);
    }
}
