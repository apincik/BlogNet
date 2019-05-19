using Blognet.Cms.Core.Articles.Models;
using Blognet.Cms.Core.Interfaces;
using MediatR;

namespace Blognet.Cms.Core.Articles.Queries.GetArticlesForSitemap
{
    public class GetArticlesForSitemapQuery : IRequest<SitemapArticleModel>, IProjectRequestQuery
    {
        public int ProjectId { get; set; }
    }
}
