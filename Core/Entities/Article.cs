using Core.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Entities
{
    [Table("cms_article")]
    public class Article : BaseEntity
    {
        [Required]
        public int ProjectId { get; set; }
        public Project Project { get; set; }

        [Required]
        [MaxLength(255)]
        public string Title { get; set; }

        public string Description { get; set; }

        public string Content { get; set; }

        [MaxLength(255)]
        [Required]
        public string Slug { get; set; }

        [MaxLength(255)]
        public string Tags { get; set; }

        public ArticleStatus Status {get; set;}


        [Required]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int? SeoId { get; set; }
        public Seo Seo { get; set; }

        public int? PhotoThumbnailId { get; set; }
        public Photo PhotoThumbnail { get; set; }
        public int? PhotoHeaderId { get; set; }
        public Photo PhotoHeader { get; set; }

        public int? AlbumId { get; set; }
        public Album Album { get; set; }

        public ArticleSettings ArticleSettings { get; set; }
    }
}
