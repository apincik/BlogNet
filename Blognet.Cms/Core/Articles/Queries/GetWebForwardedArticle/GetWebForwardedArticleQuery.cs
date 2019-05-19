using Blognet.Cms.Core.Articles.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blognet.Cms.Core.Articles.Queries.GetWebForwardedArticle
{
    public class GetWebForwardedArticleQuery : IRequest<WebArticleModel>
    {
        public string ProjectDomain { get; set; }
        public string Mask { get; set; }
    }
}
