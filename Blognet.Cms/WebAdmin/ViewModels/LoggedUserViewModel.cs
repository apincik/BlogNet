using Blognet.Cms.Core.Model;
using Blognet.Cms.Domain.Entities;
using Blognet.Cms.Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blognet.Cms.WebAdmin.ViewModels
{
    public class LoggedUserViewModel
    {
        public ApplicationUser User { get; set; }
        public ProjectDTO Project { get; set; }
    }
}
