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
    public class MenuItemService : Service<MenuItem>, IMenuItemService
    {
        public MenuItemService(IAsyncModel<MenuItem> model) : base(model)
        {

        }

        public Task<MenuItem> Create(MenuItem variable)
        {
            return Repository.AddAsync(variable);
        }

        public async Task<MenuItem> Update(MenuItem variable)
        {
            await Repository.UpdateAsync(variable);
            return variable;
        }


        public Task<List<MenuItem>> ListAllByProjectId(int id)
        {
            return Repository.Table()
                .Where(m => m.ProjectId == id)
                .ToListAsync();
        }

        public Task DeleteAsyncById(int id)
        {
            var item = Repository.Table().Find(id);
            return Repository.DeleteAsync(item);
        }
    }
}
