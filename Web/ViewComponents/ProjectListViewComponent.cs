using Core.Entities;
using Core.Interfaces;
using Core.Interfaces.Repositories;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Extensions;
using Web.ViewModels;

namespace Web.ViewComponents
{
    [ViewComponent]
    public class ProjectListViewComponent : ViewComponent
    {
        private UserManager<ApplicationUser> _userManager;
        private IProjectRepository _projectRepository;
        //private IHttpContextAccessor _httpContext;

        public ProjectListViewComponent(
            UserManager<ApplicationUser> userManager,
            IProjectRepository projectRepository)
        {
            _userManager = userManager;
            _projectRepository = projectRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            List<Project> userProjects = await _projectRepository.ListAllByUserId(user.Id);
            int? selectedProjectId = HttpContext.Request.Cookies.GetProjectId();

            return View("Default", new ProjectListViewModel {
                Projects = userProjects,
                SelectedProjectId = selectedProjectId
            });
        }
    }
}
