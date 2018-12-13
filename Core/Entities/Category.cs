using Core.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Entities
{
    [Table("cms_category")]
    public class Category : BaseEntity
    {
        [Required]
        public int ProjectId { get; set; }
        public Project Project { get; set; }

        public int? ParentCategoryId { get; set; }
        public Category ParentCategory { get; set; }
        public List<Category> SubCategories { get; set; }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }

        [Required]
        public Status Status { get; set; }

        public int? SeoId { get; set; }
        public Seo Seo { get; set; }

        public List<Article> Articles { get; set; }

        public string CategoryTree {
            get
            {
                return GetCategoryTree();
            }
        }

        private string GetCategoryTree()
        {
            StringBuilder s = new StringBuilder();
            s.Insert(0, $" {Title}");
            Category parent = ParentCategory;

            while (parent != null)
            {
                s.Insert(0, $" {parent.Title} - ");
                parent = parent.ParentCategory;
            }


            return s.ToString().Trim();
        }
    }
}
