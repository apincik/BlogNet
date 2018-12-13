using Core.Entities;
using Core.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Areas.Web.ViewModels.MenuItems
{
    public class MenuItemUpdateViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int ProjectId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Url { get; set; }

        public int? ParentMenuId { get; set; }

        public List<MenuItem> Items { get; set; }

    }
}
