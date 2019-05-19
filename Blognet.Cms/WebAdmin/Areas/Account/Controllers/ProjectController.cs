using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Blognet.Cms.WebAdmin.Extensions;
using Blognet.Cms.WebAdmin.Controllers;
using Blognet.Cms.Core.Projects.Models;
using Blognet.Cms.Core.Projects.Commands.CreateProject;
using Blognet.Cms.Core.Projects.Queries.GetProjectDetail;
using Blognet.Cms.Core.Projects.Commands.UpdateProject;
using Blognet.Cms.Core.PageForwards.Queries.GetProjectsList;
using System.Security.Claims;

namespace Blognet.Cms.WebAdmin.Areas.Account.Controllers
{
    [Authorize(Roles = "admin")]
    [Area("Account")]
    public class ProjectController : BaseController
    {
        public async Task<IActionResult> Index()
        {
            return View(
                await Mediator.Send(new GetProjectsListQuery { })
            );
        }

        public IActionResult Create()
        {
            return View(new ProjectDetailViewModel());
        }

        /// <summary>
        /// Create project.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(ProjectDetailViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View("Create", model);
            }

            await Mediator.Send(new CreateProjectCommand
            {
                DomainName = model.DomainName,
                Name = model.Name,
                UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value
            });

            return RedirectToAction("Index")
                .WithSuccess("Project", "Created successfully");
        }

        public async Task<IActionResult> Update(int id)
        {
            return View(
                await Mediator.Send(new GetProjectDetailQuery { Id = id})    
            );
        }

        /// <summary>
        /// Update Project.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Update(ProjectDetailViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("update", model);
            }

            await Mediator.Send(new UpdateProjectCommand
            {
                Id = model.Id.Value,
                Name = model.Name,
                DomainName = model.DomainName,
                Description = model.Description,
                ProjectSettings = model.ProjectSettings
            });

            return RedirectToAction("Index")
                .WithSuccess("Article", "Updated successfully");
        }

        /// <summary>
        /// Store selected project ID to session.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Select(int id)
        {
            if(HttpContext.Request.Cookies.GetProjectId() == id)
            {
                HttpContext.Response.Cookies.SetProjectId(null);
            } 
            else
            {
                HttpContext.Response.Cookies.SetProjectId(id);
            }
            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}