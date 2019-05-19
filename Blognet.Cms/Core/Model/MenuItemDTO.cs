using System;
using System.Collections.Generic;
using System.Text;

namespace Blognet.Cms.Core.Model
{
    public class MenuItemDTO
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }

        public int? ParentMenuId { get; set; }
        public MenuItemDTO ParentMenu { get; set; }
        public List<MenuItemDTO> MenuItems { get; set; }

        public int ProjectId { get; set; }
    }
}
