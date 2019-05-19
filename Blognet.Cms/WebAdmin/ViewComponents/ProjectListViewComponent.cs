using Blognet.Cms.Domain.Entities;
using Blognet.Cms.Core.Interfaces;
using Blognet.Cms.Core.Interfaces.Repositories;
using Blognet.Cms.Infrastructure.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blognet.Cms.WebAdmin.Extensions;
using Blognet.Cms.WebAdmin.ViewModels;
using MediatR;
using Blognet.Cms.Core.PageForwards.Queries.GetProjectsList;
using Blognet.Cms.Core.Model;

namespace Blognet.Cms.WebAdmin.ViewComponents
{
    [ViewComponent]
    public class ProjectListViewComponent : ViewComponent
    {
        private UserManager<ApplicationUser> _userManager;
        private IMediator _mediator;
        //private IHttpContextAccessor _httpContext;

        public ProjectListViewComponent(
            UserManager<ApplicationUser> userManager,
            IMediator mediator)
        {
            _userManager = userManager;
            _mediator = mediator;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            List<ProjectDTO> userProjects = await _mediator.Send(new GetProjectsListQuery { UserId = user.Id });
            int? selectedProjectId = HttpContext.Request.Cookies.GetProjectId();

            return View("Default", new ProjectListViewModel {
                Projects = userProjects,
                SelectedProjectId = selectedProjectId
            });
        }
    }
}
