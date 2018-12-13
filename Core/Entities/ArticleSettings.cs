using Core.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Entities
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
        public Article Article { get; set; }

        public ArticleSettings()
        {
            PageAdsDensity = PageAdsDensity.Low;
            ShowSocialPlugins = true;
            ShowComments = true;
            UpdateSlugOnTitleChange = false;
            AdsActive = true;
            Nsfw = false;
        }
    }
}
