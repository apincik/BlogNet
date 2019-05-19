using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blognet.Cms.Core.Articles.Models
{
    public class ArticleListItem
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string PhotoThumbnailImagePath { get; set; }
        public string Slug { get; set; }
    }

    public class WebCategoryArticlesModel
    {
        public List<ArticleListItem> Articles { get; set; }
        public int ArticlesCount { get; set; } = 0;
        public int Page { get; set; }
        public int Limit { get; set; }
    }
}
