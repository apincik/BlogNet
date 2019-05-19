using Blognet.Cms.Core.Model;
using Blognet.Cms.Domain.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Blognet.Cms.Core.Categories.Models
{
    public class CategoryDetailViewModel
    {
        public int? Id { get; set; }

        [Required]
        public int ProjectId { get; set; }

        [Display(Name = "Parent category")]
        public int? ParentCategoryId { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }
        
        public SeoDTO Seo { get; set; }

        public List<CategoryDTO> Categories { get; set; }
        
    }
}
