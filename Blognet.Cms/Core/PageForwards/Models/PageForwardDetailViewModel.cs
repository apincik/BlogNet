using Blognet.Cms.Core.Model;
using Blognet.Cms.Domain.Entities;
using Blognet.Cms.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Blognet.Cms.Core.PageForwards.Models
{
    public class PageForwardDetailViewModel
    {
        public int? Id { get; set; }

        [Required]
        public int ProjectId { get; set; }

        [Required]
        public string Mask { get; set; }

        public string DestinationUrl { get; set; }

        public string SourceId { get; set; }

        public PageForwardSourceType Type { get; set; }
    }
}
