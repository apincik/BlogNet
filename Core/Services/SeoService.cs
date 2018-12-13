using Core.Entities;
using Core.Enum;
using Core.Exceptions;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class SeoService : Service<Seo>, ISeoService
    {
        public SeoService(IAsyncModel<Seo> model) : base(model)
        {
        }

        public async Task<Seo> Create(Seo seo)
        {
            if (!seo.IsEmpty())
            {
                return await Repository.AddAsync(seo);
            }
            else
            {
                return null;
            }
        }

        public async Task<Seo> Update(Seo seo)
        {
            if(seo.Id == 0)
            {
                throw new EmptyDatabaseEntityException("Trying to update nonexisting entity of type Seo.");
            }

            await Repository.UpdateAsync(seo);
            return seo;
        }
    }
}
