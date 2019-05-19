using System.Threading.Tasks;
using Blognet.Cms.Core.PageForwards.Commands.CreatePageForward;
using Blognet.Cms.Core.PageForwards.Commands.DeletePageForward;
using Blognet.Cms.Core.PageForwards.Models;
using Blognet.Cms.Core.PageForwards.Queries.GetPageForwardsList;
using Microsoft.AspNetCore.Mvc;
using Blognet.Cms.WebAdmin.Controllers;
using Blognet.Cms.WebAdmin.Extensions;
using Blognet.Cms.WebAdmin.Filters;
using Microsoft.AspNetCore.Authorization;
using Blognet.Cms.Core.PageForwards.Commands.UpdatePageForward;
using Blognet.Cms.Core.PageForwards.Queries.GetPageForwardDetail;

namespace Blognet.Cms.WebAdmin.Areas.Web.Controllers
{
    [Area("Web")]
    [Authorize(Roles = "admin")]
    public class PageForwardController : BaseController
    {   
        [CheckProjectSet]
        public async Task<IActionResult> Index()
        {
            return View(
                await Mediator.Send(
                    new GetPageForwardsListQuery{
                        ProjectId = HttpContext.Request.Cookies.GetProjectId().Value
                    }
                )
            );
        }

        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeletePageForwardCommand {Id = id});

            return RedirectToAction("Index");
        }

        [CheckProjectSet]
        public IActionResult Create()
        {
            return View(new PageForwardDetailViewModel()
            {
                ProjectId = HttpContext.Request.Cookies.GetProjectId().Value,
            });
        }

        /// <summary>
        /// Create category.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(PageForwardDetailViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("create", model)
                    .WithWarning("Info", "Validation error, cannot be saved.");
            }

            await Mediator.Send(new CreatePageForwardCommand
            {
                ProjectId = model.ProjectId,
                Mask =  model.Mask,
                DestinationUrl = model.DestinationUrl,
                SourceId = model.SourceId,
                Type = model.Type
            });

            return RedirectToAction("Index")
                .WithSuccess("Page forward", "Created successfully.");
        }

        [CheckProjectSet]
        public async Task<IActionResult> Update(int id)
        {
            return View(
                await Mediator.Send(new GetPageForwardDetailQuery { Id = id })
            );
        }

        /// <summary>
        /// Create category.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Update(PageForwardDetailViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Update", new { id = model.Id.Value })
                    .WithWarning("Invalid data.", "Cannot be saved.");
            }

            await Mediator.Send(new UpdatePageForwardCommand
            {
                Id = model.Id.Value,
                ProjectId = model.ProjectId,
                Mask = model.Mask,
                DestinationUrl = model.DestinationUrl,
                SourceId = model.SourceId,
                Type = model.Type
            });

            return RedirectToAction("Index")
                .WithSuccess("Page forward", "Updated successfully.");
        }
    }
}