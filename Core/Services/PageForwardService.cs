using Core.Entities;
using Core.Enum;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class PageForwardService : Service<PageForward>, IPageForwardService
    {
        public PageForwardService(IAsyncModel<PageForward> model) : base(model)
        {

        }

        public Task<PageForward> Create(PageForward forward)
        {
            return Repository.AddAsync(forward);
        }

        public async Task<PageForward> Update(PageForward forward)
        {
            await Repository.UpdateAsync(forward);
            return forward;
        }
    
        public Task<List<PageForward>> ListAllByProjectId(int id)
        {
            return Repository.Table()
                .Where(m => m.ProjectId == id)
                .ToListAsync();
        }

        public async Task DeleteById(int id)
        {
            PageForward forward = await Repository.Table().FindAsync(id);
            await Repository.DeleteAsync(forward);
        }
    }
}
