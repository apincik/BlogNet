using Blognet.Cms.Core.Articles.Models;
using Blognet.Cms.Core.Interfaces;
using Blognet.Cms.Core.Model;
using Domain.Enum;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blognet.Cms.Core.Articles.Queries.GetArticlesList
{
    public class GetArticlesListQuery : IRequest<List<ArticleDTO>>, IProjectRequestQuery, IFilterRequestQuery
    {
        public int? Page { get; set; }
        public int? Limit { get; set; }
        public Order? Order { get; set; }
        public string OrderByColumnName { get; set; }
        public int ProjectId { get; set; }
    }
}
