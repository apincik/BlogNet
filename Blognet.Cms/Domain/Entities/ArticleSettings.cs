using Blognet.Cms.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Blognet.Cms.Domain.Entities
{
    [Table("cms_article_settings")]
    public class ArticleSettings : BaseEntity
    {
        [Display(Name = "Ads Density")]
        public PageAdsDensity PageAdsDensity { get; set; }

        [Display(Name = "Social Plugins")]
        public bool ShowSocialPlugins { get; set; }

        [Display(Name = "Show Comments")]
        public bool ShowComments { get; set; }

        [Display(Name = "Update Slug")]
        public bool UpdateSlugOnTitleChange { get; set; }

        [Display(Name = "Ads Active")]
        public bool AdsActive { get; set; }

        public bool Nsfw { get; set; }

        public int ArticleId { get; set; }
    }
}
