using Core.Entities;
using Core.Enum;
using Core.Exceptions;
using Core.Interfaces;
using Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class SeoService : ISeoService
    {
        private ISeoRepository _seoRepository;

        public SeoService(ISeoRepository seoRepository)
        {
            _seoRepository = seoRepository;
        }

        public Task Create(Seo seo)
        {
            if (!seo.IsEmpty())
            {
                return _seoRepository.Add(seo);
            }
            else
            {
                return null;
            }
        }

        public Task Update(Seo seo)
        {
            if(seo.Id == 0)
            {
                throw new EmptyDatabaseEntityException("Trying to update nonexisting entity of type Seo.");
            }

            return _seoRepository.Update(seo);
        }
    }
}
