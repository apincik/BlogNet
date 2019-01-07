using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Model;

namespace WebApi.CommandResults
{
    public class CategoryArticlesResult
    {
        public List<ArticleListItem> Articles { get; set; }
        public int ArticlesCount { get; set; } = 0;
        public int Page { get; set; }
        public int Limit { get; set; }
    }
}
