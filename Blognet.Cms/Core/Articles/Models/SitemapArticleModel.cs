using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blognet.Cms.Core.Articles.Models
{
    public class SitemapArticle
    {
        public string Slug { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class SitemapArticleModel
    {
        public List<SitemapArticle> Articles { get; set; }
    }
}
