using Core.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web.Areas.Cms.ViewModels.Categories
{
    public class CategoryUpdateViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int ProjectId { get; set; }

        [Display(Name = "Parent category")]
        public string ParentCategoryId { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }
        
        public Seo Seo { get; set; }

        public List<Core.Entities.Category> Categories { get; set; }
        
    }
}
