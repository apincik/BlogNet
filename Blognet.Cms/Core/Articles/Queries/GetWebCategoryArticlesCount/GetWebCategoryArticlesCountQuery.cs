using Blognet.Cms.Core.Articles.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blognet.Cms.Core.Articles.Queries.GetWebCategoryArticlesCount
{
    public class GetWebCategoryArticlesCountQuery : IRequest<int>
    {
        public string ProjectDomain { get; set; }
        public string CategoryTitle { get; set; }
    }
}
