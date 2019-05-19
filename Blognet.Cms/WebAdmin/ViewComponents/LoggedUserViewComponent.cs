using Blognet.Cms.Infrastructure.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Blognet.Cms.WebAdmin.Extensions;
using Blognet.Cms.WebAdmin.ViewModels;
using MediatR;
using Blognet.Cms.Core.Model;
using Blognet.Cms.Core.Projects.Queries.GetProjectDetail;

namespace Blognet.Cms.WebAdmin.ViewComponents
{
    [ViewComponent]
    public class LoggedUserViewComponent : ViewComponent
    {
        private UserManager<ApplicationUser> _userManager;
        private IMediator _mediator;
        //private IHttpContextAccessor _httpContext;

        public LoggedUserViewComponent(
            UserManager<ApplicationUser> userManager,
            IMediator mediator)
        {
            _userManager = userManager;
            _mediator = mediator;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            ProjectDTO project = null;
            int? selectedProjectId = HttpContext.Request.Cookies.GetProjectId();
            if(selectedProjectId != null)
            {
                project = await _mediator.Send(new GetProjectQuery { Id = selectedProjectId.Value });
            }

            var user = await _userManager.GetUserAsync(HttpContext.User);

            return View("Default", new LoggedUserViewModel()
            {
                User = user,
                Project = project
            });
        }
    }
}
