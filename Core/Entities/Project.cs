using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Entities
{
    [Table("user_project")]
    public class Project : BaseEntity
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        [Required]
        [StringLength(30)]
        public string DomainName { get; set; }

        public string Description { get; set; }

        public List<Category> Categories { get; set; }

        public List<Article> Articles { get; set; }

        public ProjectSettings ProjectSettings { get; set; }

        public List<MenuItem> MenuItems { get; set; }

        /*private Project() { }
        public Project(string userId, string name, string domainName, string description)
        {
            UserId = userId;
            Name = name;
            DomainName = domainName;
            Description = description;
        }*/
    }
}
