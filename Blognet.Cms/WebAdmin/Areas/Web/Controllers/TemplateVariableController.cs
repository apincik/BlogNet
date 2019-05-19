using System.Linq;
using System.Threading.Tasks;
using Blognet.Cms.Core.TemplateVariables.Commands.CreateTemplateVariable;
using Blognet.Cms.Core.TemplateVariables.Commands.ToggleTemplateVariableStatus;
using Blognet.Cms.Core.TemplateVariables.Commands.UpdateTemplateVariable;
using Blognet.Cms.Core.TemplateVariables.Models;
using Blognet.Cms.Core.TemplateVariables.Queries.GetTemplateVariableDetail;
using Blognet.Cms.Core.TemplateVariables.Queries.GetTemplateVariablesList;
using Microsoft.AspNetCore.Mvc;
using Blognet.Cms.WebAdmin.Controllers;
using Blognet.Cms.WebAdmin.Extensions;
using Blognet.Cms.WebAdmin.Filters;
using Microsoft.AspNetCore.Authorization;
using Blognet.Cms.Core.TemplateVariables.Commands.DeleteTemplateVariable;
using Blognet.Cms.Domain.Exceptions;

namespace Blognet.Cms.WebAdmin.Areas.Web.Controllers
{
    [Area("Web")]
    [Authorize(Roles = "admin")]
    public class TemplateVariableController : BaseController
    {
        [CheckProjectSet]
        public async Task<IActionResult> Index()
        {
            return View(await Mediator.Send(new GetTemplateVariablesListQuery { ProjectId = HttpContext.Request.Cookies.GetProjectId().Value }));

        }

        public async Task<IActionResult> ToggleStatus(int id)
        {
            await Mediator.Send(new ToggleTemplateVariableStatusCommand {Id = id});
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await Mediator.Send(new DeleteTemplateVariableCommand { Id = id });
            }
            catch (EntityDeleteException e)
            {
                return RedirectToAction("Index")
                    .WithWarning("Template Variable", e.Message);
            }

            return RedirectToAction("Index")
                .WithSuccess("Template Variable", "Deleted succesfuly.");
        }

        [CheckProjectSet]
        public async Task<IActionResult> Create()
        {
            int projectId = HttpContext.Request.Cookies.GetProjectId().Value;
            var model = new TemplateVariableDetailViewModel
            {
                ProjectId = projectId,
                Variables = await Mediator.Send(new GetTemplateVariablesListQuery { ProjectId = projectId })
            };

            return View(model);
        }

        /// <summary>
        /// Create category.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(TemplateVariableDetailViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Variables = await Mediator.Send(new GetTemplateVariablesListQuery { ProjectId = model.ProjectId });
                return View("create", model)
                    .WithWarning("Info", "Error occured, cannot be saved.");
            }

            await Mediator.Send(new CreateTemplateVariableCommand
            {
                ProjectId = model.ProjectId,
                Label = model.Label,
                Content = model.Content,
                ParentTemplateVariableId = model.ParentTemplateVariableId,
                ShowRaw = model.ShowRaw,
                Type = model.Type
            });

            return RedirectToAction("Index")
                .WithSuccess("Variable", "Created successfully.");
        }

        [CheckProjectSet]
        public async Task<IActionResult> Update(int id)
        {
            var model = await Mediator.Send(new GetTemplateVariableDetailQuery {Id = id});
            model.Variables = (await Mediator.Send(new GetTemplateVariablesListQuery {ProjectId = model.ProjectId}))
                .Where(x => x.Id != model.Id)
                .ToList();

            return View(model);
        }

        /// <summary>
        /// Create category.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Update(TemplateVariableDetailViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Variables = await Mediator.Send(new GetTemplateVariablesListQuery { ProjectId = model.ProjectId });
                return RedirectToAction("Update", model)
                    .WithWarning("Invalid data.", "Cannot be saved.");
            }

            await Mediator.Send(new UpdateTemplateVariableCommand
            {
                Id = model.Id,
                ProjectId = model.ProjectId,
                Label = model.Label,
                Content = model.Content,
                ParentTemplateVariableId = model.ParentTemplateVariableId,
                ShowRaw = model.ShowRaw,
                Type = model.Type
            });

            return RedirectToAction("Index")
                .WithSuccess("Variable", "Updated successfully.");
        }

    }
}