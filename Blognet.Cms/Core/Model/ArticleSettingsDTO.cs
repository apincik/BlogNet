using Blognet.Cms.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blognet.Cms.Core.Model
{
    public class ArticleSettingsDTO
    {
        public int? Id { get; set; }

        [Required]
        public PageAdsDensity PageAdsDensity { get; set; }

        public bool ShowSocialPlugins { get; set; }

        public bool ShowComments { get; set; }

        public bool UpdateSlugOnTitleChange { get; set; }

        public bool AdsActive { get; set; }

        public bool Nsfw { get; set; }

        public int? ArticleId { get; set; }

        public ArticleSettingsDTO()
        {
            PageAdsDensity = Domain.Enum.PageAdsDensity.Default;
            ShowSocialPlugins = true;
            ShowComments = true;
            UpdateSlugOnTitleChange = false;
            AdsActive = true;
            Nsfw = false;
        }
    }
}
