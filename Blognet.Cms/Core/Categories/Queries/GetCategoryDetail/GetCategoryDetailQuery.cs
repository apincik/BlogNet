using Blognet.Cms.Core.Categories.Models;
using Blognet.Cms.Core.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blognet.Cms.Core.Categories.Queries.GetCategoryDetail
{
    public class GetCategoryDetailQuery : IRequest<CategoryDetailViewModel>
    {
        public int? Id { get; set; }

        public int ProjectId { get; set; }

        public int? ParentCategoryId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public SeoDTO Seo { get; set; }
    }
}
