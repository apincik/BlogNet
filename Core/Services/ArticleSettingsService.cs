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
    public class ArticleSettingsService : Service<ArticleSettings>, IArticleSettingsService
    {
        public ArticleSettingsService(IAsyncModel<ArticleSettings> model, ISeoService seoService) : base(model)
        {
        }

        public async Task<ArticleSettings> Create(ArticleSettings settings)
        {
            return await Repository.AddAsync(settings);
        }

        public async Task Update(ArticleSettings articleSettings)
        {
            await Repository.UpdateAsync(articleSettings);
        }
    }
}
