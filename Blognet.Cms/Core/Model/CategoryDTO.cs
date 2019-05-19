using Blognet.Cms.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blognet.Cms.Core.Model
{
    public class CategoryDTO
    {
        public int? Id { get; set; }

        public int? ProjectId { get; set; }
        
        public int? ParentCategoryId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public Status Status { get; set; }

        public int? SeoId { get; set; }

        public ProjectDTO Project { get; set; }

        public CategoryDTO ParentCategory { get; set; }

        public List<CategoryDTO> SubCategories { get; set; }

        public SeoDTO Seo { get; set; }

        public string CategoryLabelsTree
        {
            get
            {
                StringBuilder s = new StringBuilder();
                s.Insert(0, $" {Title}");
                CategoryDTO parent = ParentCategory;

                while (parent != null)
                {
                    s.Insert(0, $" {parent.Title} - ");
                    parent = parent.ParentCategory;
                }

                return s.ToString().Trim();
            }
        }
    }
}
