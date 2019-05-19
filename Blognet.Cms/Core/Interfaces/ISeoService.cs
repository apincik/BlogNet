using Blognet.Cms.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blognet.Cms.Core.Interfaces
{
    public interface ISeoService
    {
        Task Create(Seo project);
        Task Update(Seo project);
    }
}
