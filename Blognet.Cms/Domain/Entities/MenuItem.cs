using Blognet.Cms.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Blognet.Cms.Domain.Entities
{
    [Table("web_menu_item")]
    public class MenuItem : BaseEntity
    {
        [MaxLength(255)]
        public string Title { get; set; }
        [MaxLength(255)]
        public string Url { get; set; }

        public int? ParentMenuId { get; set; }
        public MenuItem ParentMenu { get; set; }
        public List<MenuItem> MenuItems { get; set; }

        public int ProjectId { get; set; }
        public Project Project { get; set; }

    }
}
