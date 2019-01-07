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
    public class PageForwardService : IPageForwardService
    {
        private IPageForwardRepository _pageForwardRepository;

        public PageForwardService(IPageForwardRepository pageForwardRepository)
        {
            _pageForwardRepository = pageForwardRepository;
        }

        public Task Create(PageForward forward)
        {
            return _pageForwardRepository.Add(forward);
        }

        public Task Update(PageForward forward)
        {
            return _pageForwardRepository.Update(forward);
        }

        public async Task DeleteById(int id)
        {
            PageForward forward = await _pageForwardRepository.Get(id);
            await _pageForwardRepository.Delete(forward);
        }
    }
}
