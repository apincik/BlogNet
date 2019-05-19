using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blognet.Cms.Core.Model
{
    public class ProjectSettingsDTO
    {
        public int? Id { get; set; }
        public int? ArticleThumbnailWidth { get; set; }
        public int? ArticleThumbnailHeight { get; set; }
        public int? ArticleHeaderWidth { get; set; }
        public int? ArticleHeaderHeight { get; set; }

        public int ProjectId { get; set; }

        public Point ThumbnailDimension
        {
            get
            {
                if (ArticleThumbnailWidth == null || ArticleThumbnailHeight == null) {
                    return new Point(0, 0);
                }

                return new Point(ArticleThumbnailWidth.Value, ArticleThumbnailHeight.Value);
            }
           
        }

        public Point HeaderDimension
        {
            get
            {
                if (ArticleHeaderWidth == null || ArticleHeaderHeight == null)
                {
                    return new Point(0, 0);
                }

                return new Point(ArticleHeaderWidth.Value, ArticleHeaderHeight.Value);
            }
        }
    }
}
