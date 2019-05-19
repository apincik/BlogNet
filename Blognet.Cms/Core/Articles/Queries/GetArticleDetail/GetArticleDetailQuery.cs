using Blognet.Cms.Core.Articles.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blognet.Cms.Core.Articles.Queries.GetArticleDetail
{
    public class GetArticleDetailQuery : IRequest<ArticleDetailViewModel>
    {
        public int Id;
    }
}
