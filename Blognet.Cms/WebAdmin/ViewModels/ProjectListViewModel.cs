using Blognet.Cms.Core.Model;
using Blognet.Cms.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blognet.Cms.WebAdmin.ViewModels
{
    public class ProjectListViewModel
    {
        public List<ProjectDTO> Projects;
        public int? SelectedProjectId;
    }
}
