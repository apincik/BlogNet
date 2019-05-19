using System.Linq;
using System.Threading.Tasks;
using Blognet.Cms.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Blognet.Cms.WebAdmin.Extensions;
using Blognet.Cms.WebAdmin.Filters;
using Microsoft.AspNetCore.Authorization;
using Blognet.Cms.WebAdmin.Controllers;
using Blognet.Cms.Core.Categories.Queries.GetCategoriesList;
using Domain.Enum;
using Blognet.Cms.Core.Categories.Commands.ToggleCategoryStatus;
using Blognet.Cms.Core.Categories.Models;
using Blognet.Cms.Core.Categories.Commands.CreateCategory;
using Blognet.Cms.Core.Categories.Queries;
using Blognet.Cms.Core.Categories.Commands.UpdateCategory;
using Blognet.Cms.Core.Categories.Queries.GetCategoryDetail;
using Blognet.Cms.Core.Categories.Commands.DeleteCategory;
using Blognet.Cms.Domain.Exceptions;

namespace Blognet.Cms.WebAdmin.Areas.Cms.Controllers
{
    [Area("Cms")]
    [Authorize(Roles = "admin")]
    public class CategoryController : BaseController
    {
        /// <summary>
        /// Listing categories by set project.
        /// </summary>
        /// <returns></returns>
        [CheckProjectSet]
        public async Task<IActionResult> Index()
        {
            return View(
                await Mediator.Send(new GetCategoriesListQuery
                {
                    ProjectId = HttpContext.Request.Cookies.GetProjectId().Value,
                    Page = 0,
                    Limit = 10,
                    Order = Order.DESC,
                    OrderByColumnName = nameof(Album.Id)
                })
            );
        }

        /// <summary>
        /// Update category status.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> ToggleStatus(int id)
        {
            await Mediator.Send(new ToggleCategoryStatusCommand { Id = id });
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await Mediator.Send(new DeleteCategoryCommand { Id = id });
            }
            catch (EntityDeleteException e)
            {
                return RedirectToAction("Index")
                    .WithWarning("Category", e.Message);
            }

            return RedirectToAction("Index")
                .WithSuccess("Category", "Deleted succesfuly.");
        }

        /// <summary>
        /// Create form page for category.
        /// </summary>
        /// <returns></returns>
        [CheckProjectSet]
        public async Task<IActionResult> Create()
        {
            int projectId = HttpContext.Request.Cookies.GetProjectId().Value;
            var model = new CategoryDetailViewModel()
            {
                ProjectId = projectId,
                Categories = await Mediator.Send(new GetCategoriesListQuery { ProjectId = projectId })
            };
            
            return View(model);
        }

        /// <summary>
        /// Create category.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(CategoryDetailViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Create")
                    .WithWarning("Invalid data.", "Cannot be saved.");
            }

            await Mediator.Send(new CreateCategoryCommand
            {
                Title = model.Title,
                Description = model.Description,
                ProjectId = model.ProjectId,
                ParentCategoryId = model.ParentCategoryId,
                Seo = model.Seo
            });

            return RedirectToAction("Index")
                .WithSuccess("Category", "Created successfully");
        }

        /// <summary>
        /// Update form page.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [CheckProjectSet]
        public async Task<IActionResult> Update(int id)
        {
            var model = await Mediator.Send(new GetCategoryDetailQuery { Id = id });
            model.Categories = (await Mediator.Send(new GetCategoriesListQuery { ProjectId = model.ProjectId }))
                                        .Where(x => x.Id.Value != id).ToList();

            return View(model);
        }

        /// <summary>
        /// Create category.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Update(GetCategoryDetailQuery model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Update", new { id = model.Id.Value })
                    .WithWarning("Invalid data.", "Cannot be saved.");
            }

            await Mediator.Send(new UpdateCategoryCommand
            {
                Id = model.Id.Value,
                Title = model.Title,
                Description = model.Description,
                ProjectId = model.ProjectId,
                ParentCategoryId = model.ParentCategoryId,
                Seo = model.Seo
            });

            return RedirectToAction("Index").WithSuccess("Category", "Updated successfully");
        }

    }
}