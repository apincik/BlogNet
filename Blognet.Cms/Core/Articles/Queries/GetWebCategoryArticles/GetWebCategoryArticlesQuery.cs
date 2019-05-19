using Blognet.Cms.Core.Articles.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blognet.Cms.Core.Articles.Queries.GetWebCategoryArticles
{
    public class GetWebCategoryArticlesQuery : IRequest<WebCategoryArticlesModel>
    {
        public string ProjectDomain { get; set; }
        public string CategoryTitle { get; set; }
        public int Page { get; set; } = 0;
        public int Limit { get; set; } = 10;
    }
}
