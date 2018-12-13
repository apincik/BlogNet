using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Areas.Account.ViewModels.Projects
{
    public class ProjectCreateViewModel
    {

        [Required(ErrorMessage = "Fill in project name.")]
        [Display(Name = "Project name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Fill in domain name.")]
        [Display(Name = "Domain name")]
        public string DomainName { get; set; }

    }
}
