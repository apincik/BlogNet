using Blognet.Cms.Core.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Blognet.Cms.Core.Projects.Models
{
    public class ProjectDetailViewModel
    {
        
        public int? Id { get; set; }

        [Required(ErrorMessage = "Fill in project name.")]
        [Display(Name = "Project name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Fill in domain name.")]
        [Display(Name = "Domain name")]
        public string DomainName { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        public ProjectSettingsDTO ProjectSettings { get; set; }
}
}
