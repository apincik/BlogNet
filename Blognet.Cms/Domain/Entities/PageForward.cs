using Blognet.Cms.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Blognet.Cms.Domain.Entities
{
    [Table("web_page_forward")]
    public class PageForward : BaseEntity
    {
        [MaxLength(255)]
        public string Mask { get; set; }
        [MaxLength(255)]
        public string DestinationUrl { get; set; }
        // Could be ID or Slug
        [MaxLength(255)]
        public string SourceId { get; set; }
        public PageForwardSourceType Type { get; set; }

        public int ProjectId { get; set; }
        public Project Project { get; set; }

    }
}
