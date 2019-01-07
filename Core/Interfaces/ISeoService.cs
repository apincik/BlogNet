using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ISeoService
    {
        Task Create(Seo project);
        Task Update(Seo project);
    }
}
