using Blognet.Cms.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Text;

namespace Blognet.Cms.Domain.Entities
{
    [Table("user_project_settings")]
    public class ProjectSettings : BaseEntity
    {
        public int? ArticleThumbnailWidth { get; set; }
        public int? ArticleThumbnailHeight { get; set; }
        public int? ArticleHeaderWidth { get; set; }
        public int? ArticleHeaderHeight { get; set; }

        public int ProjectId { get; set; }
        public Project Project { get; set; }

        public ProjectSettings()
        {
            // Set init values...
        }

        public Point GetThumbnailDimension()
        {
            return new Point(ArticleThumbnailWidth.Value, ArticleThumbnailHeight.Value);
        }

        public Point GetHeaderDimension()
        {
            return new Point(ArticleHeaderWidth.Value, ArticleHeaderHeight.Value);
        }
    }
}
