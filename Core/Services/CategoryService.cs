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
    public class CategoryService : ICategoryService
    {
        private ICategoryRepository _categoryRepository;
        private ISeoService _seoService;

        public CategoryService(ICategoryRepository categoryRepository, ISeoService seoService)
        {
            _categoryRepository = categoryRepository;
            _seoService = seoService;
        }

        public async Task ToggleStatusById(int id)
        {
            Category cat = await _categoryRepository.Get(id);
            cat.Status = cat.Status == Status.Inactive ? Status.Active : Status.Inactive;
            await _categoryRepository.Update(cat);
        }

        public Task Create(Category category)
        {
            if (category.Seo.IsEmpty())
            {
                category.Seo = null;
                category.SeoId = null;
            }

            return _categoryRepository.Add(category);
        }

        public Task Update(Category category)
        {
            if (category.Seo.IsEmpty() && category.Seo.Id == 0)
            {
                category.Seo = null;
                category.SeoId = null;
            }

            return _categoryRepository.Update(category);
        }
    }
}
