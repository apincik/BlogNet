using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ISeoService : IService<Seo>
    {
        Task<Seo> Create(Seo project);
        Task<Seo> Update(Seo project);
    }
}
