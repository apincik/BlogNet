using Blognet.Cms.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blognet.Cms.Core.Model
{
    public class ArticleDTO
    {
        public int? Id { get; set; }
        public int? ProjectId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Content { get; set; }

        public string Slug { get; set; }
        public string Tags { get; set; }

        public ArticleStatus Status { get; set; }

        public int? CategoryId { get; set; }

        public int? SeoId { get; set; }

        public int? PhotoThumbnailId { get; set; }
        
        public int? PhotoHeaderId { get; set; }

        public int? AlbumId { get; set; }

        public ProjectDTO Project { get; set; }

        public CategoryDTO Category { get; set; }

        public AlbumDTO Album { get; set; }

        public PhotoDTO PhotoThumbnail { get; set; }

        public PhotoDTO PhotoHeader { get; set; }

        public SeoDTO Seo { get; set; }

        public ArticleSettingsDTO ArticleSettings { get; set; }
    }
}
