using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Blognet.Cms.WebAdmin.Extensions;
using Blognet.Cms.WebAdmin.Filters;
using Microsoft.AspNetCore.Authorization;
using Blognet.Cms.WebAdmin.Controllers;
using Blognet.Cms.Core.MenuItems.Queries.GetMenuItemsList;
using Blognet.Cms.Core.MenuItems.Commands.DeleteMenuItem;
using Blognet.Cms.Core.MenuItems.Models;
using Blognet.Cms.Core.MenuItems.Commands.CreateMenuItem;
using Blognet.Cms.Core.MenuItems.Commands.UpdateMenuItem;
using Blognet.Cms.Core.MenuItems.Queries.GetMenuItemDetail;
using Blognet.Cms.Domain.Exceptions;

namespace Blognet.Cms.WebAdmin.Areas.Web.Controllers
{
    [Area("Web")]
    [Authorize(Roles = "admin")]
    public class MenuController : BaseController
    {        
        [CheckProjectSet]
        public async Task<IActionResult> Index()
        {
            return View(await Mediator.Send(new GetMenuItemsListQuery
            {
                ProjectId = HttpContext.Request.Cookies.GetProjectId().Value
            }));
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await Mediator.Send(new DeleteMenuItemCommand { Id = id });
            }
            catch(EntityDeleteException e)
            {
                return RedirectToAction("Index")
                    .WithWarning("Menu", e.Message);
            }

            return RedirectToAction("Index")
                .WithSuccess("Menu", "Deleted succesfuly.");
        }

        [CheckProjectSet]
        public async Task<IActionResult> Create()
        {
            int projectId = HttpContext.Request.Cookies.GetProjectId().Value;
            var model = new MenuItemDetailViewModel()
            {
                ProjectId = projectId,
                Items = await Mediator.Send(new GetMenuItemsListQuery { ProjectId = projectId })
            };

            return View(model);
        }

        /// <summary>
        /// Create category.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(MenuItemDetailViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Items = await Mediator.Send(new GetMenuItemsListQuery { ProjectId = model.ProjectId });
                return View("Create", model)
                    .WithInfo("Invalid data", "Cannot be saved.");
            }

            await Mediator.Send(new CreateMenuItemCommand
            {
                Title = model.Title,
                ProjectId = model.ProjectId,
                ParentMenuId = model.ParentMenuId,
                Url = model.Url
            });

            return RedirectToAction("Index")
                .WithSuccess("Menu", "Created successfully");
        }

        [CheckProjectSet]
        public async Task<IActionResult> Update(int id)
        {
            int projectId = HttpContext.Request.Cookies.GetProjectId().Value;

            MenuItemDetailViewModel model = await Mediator.Send(new GetMenuItemDetailQuery {Id = id});
            model.Items = (
                    await Mediator.Send(
                    new GetMenuItemsListQuery {ProjectId = projectId}))
                .Where(x => x.Id.Value != id)
                .ToList();

            return View(model);
        }

        /// <summary>
        /// Create category.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Update(MenuItemDetailViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Items = await Mediator.Send(new GetMenuItemsListQuery { ProjectId = model.ProjectId });
                return RedirectToAction("Update", model)
                    .WithWarning("Invalid data.", "Cannot be saved.");
            }

            await Mediator.Send(new UpdateMenuItemCommand
            {
                Id = model.Id,
                Title = model.Title,
                ProjectId = model.ProjectId,
                ParentMenuId = model.ParentMenuId,
                Url = model.Url
            });

            return RedirectToAction("Index")
                .WithSuccess("Menu", "Updated successfully");
        }

    }
}