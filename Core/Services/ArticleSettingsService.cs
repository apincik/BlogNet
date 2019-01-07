using Core.Entities;
using Core.Enum;
using Core.Interfaces;
using Core.Interfaces.Repositories;
using System.Threading.Tasks;

namespace Core.Services
{
    public class ArticleSettingsService : IArticleSettingsService
    {
        private IArticleSettingsRepository _articleSettingsRepository;

        public ArticleSettingsService(IArticleSettingsRepository articleSettingsRepository)
        {
            _articleSettingsRepository = articleSettingsRepository;
        }

        public Task Create(ArticleSettings settings)
        {
            return _articleSettingsRepository.Add(settings);
        }

        public async Task Update(ArticleSettings articleSettings)
        {
            await _articleSettingsRepository.Update(articleSettings);
        }
    }
}
