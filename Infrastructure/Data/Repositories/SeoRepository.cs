using Core.Entities;
using Core.Interfaces;
using Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class SeoRepository : Repository<Seo>, ISeoRepository
    {
        public SeoRepository(WebContext dbContext) : base(dbContext)
        {
        }
    }
}
