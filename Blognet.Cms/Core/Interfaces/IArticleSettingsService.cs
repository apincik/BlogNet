using Blognet.Cms.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blognet.Cms.Core.Interfaces
{
    public interface IArticleSettingsService
    {
        Task Create(ArticleSettings category);
        Task Update(ArticleSettings category);
    }
}
