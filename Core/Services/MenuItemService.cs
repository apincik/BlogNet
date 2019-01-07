using Core.Entities;
using Core.Enum;
using Core.Interfaces;
using Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class MenuItemService : IMenuItemService
    {
        private IMenuItemRepository _menuItemRepository;

        public MenuItemService(IMenuItemRepository menuItemRepository)
        {
            _menuItemRepository = menuItemRepository;
        }

        public Task Create(MenuItem variable)
        {
            return _menuItemRepository.Add(variable);
        }

        public Task Update(MenuItem variable)
        {
             return _menuItemRepository.Update(variable);
        }

        public async Task Delete(int id)
        {
            var item = await _menuItemRepository.Get(id);
            await _menuItemRepository.Delete(item);
        }
    }
}
