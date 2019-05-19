using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Blognet.Cms.Domain.Entities
{
    [Table("seo_onpage")]
    public class Seo : BaseEntity
    {
        [MaxLength(255)]
        public string Title { get; set; }
        [MaxLength(255)]
        public string Description { get; set; }
        [MaxLength(255)]
        public string Keywords { get; set; }

        public bool IsEmpty()
        {
            return !(Title != null || Description != null || Keywords != null);
        }
    }
}
