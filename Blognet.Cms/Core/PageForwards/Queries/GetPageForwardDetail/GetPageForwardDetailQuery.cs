using Blognet.Cms.Core.Model;
using MediatR;

using Blognet.Cms.Core.PageForwards.Models;

namespace Blognet.Cms.Core.PageForwards.Queries.GetPageForwardDetail
{
    public class GetPageForwardDetailQuery : IRequest<PageForwardDetailViewModel>
    {
        public int Id;

        public int ProjectId { get; set; }

        public string ParentCategoryId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public SeoDTO Seo { get; set; }
    }
}
