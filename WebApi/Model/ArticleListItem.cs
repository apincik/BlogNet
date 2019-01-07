using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Model
{
    /// <summary>
    /// DTO
    /// </summary>
    public class ArticleListItem
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string PhotoThumbnailImagePath { get; set; }
        public string Slug { get; set; }
    }
}
