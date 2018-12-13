using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.ViewModels
{
    public class ProjectListViewModel
    {
        public List<Project> Projects;
        public int? SelectedProjectId;
    }
}
