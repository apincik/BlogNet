using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blognet.Cms.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Blognet.Cms.WebAdmin.Extensions;
using Blognet.Cms.WebAdmin.Filters;
using Microsoft.AspNetCore.Authorization;
using Blognet.Cms.Core.Articles.Models;
using Blognet.Cms.WebAdmin.Controllers;
using Blognet.Cms.Core.Articles.Commands.CreateArticle;
using Blognet.Cms.Core.Articles.Commands.ToggleArticleStatus;
using Blognet.Cms.Core.Articles.Queries.GetArticleDetail;
using Blognet.Cms.Core.Articles.Commands.UpdateArticle;
using Blognet.Cms.Core.Articles.Queries.GetArticlesList;
using Domain.Enum;
using Blognet.Cms.Core.Categories.Queries.GetCategoriesList;
using Blognet.Cms.Core.Articles.Commands.DeleteArticle;

namespace Blognet.Cms.WebAdmin.Areas.Cms.Controllers
{
    [Area("Cms")]
    [Authorize(Roles = "admin")]
    public class ArticleController : BaseController
    {
        [CheckProjectSet]
        public async Task<IActionResult> Index()
        {
            int projectId = HttpContext.Request.Cookies.GetProjectId().Value;
            var articles = await Mediator.Send(new GetArticlesListQuery { ProjectId = projectId, OrderByColumnName = nameof(Article.Id), Order = Order.DESC });
            return View(articles);
        }

        public async Task<IActionResult> ToggleStatus(int id)
        {
            await Mediator.Send(new ToggleArticleStatusCommand { Id = id });
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteArticleCommand { Id = id });
            return RedirectToAction("Index");
        }

        [CheckProjectSet]
        public async Task<IActionResult> Create()
        {
            int projectId = HttpContext.Request.Cookies.GetProjectId().Value;
            var model = new ArticleDetailViewModel()
            {
                ProjectId = projectId,
                Categories = await Mediator.Send(new GetCategoriesListQuery { ProjectId = projectId })
            };

            return View(model);
        }

        /// <summary>
        /// Create Article.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(ArticleDetailViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Create")
                    .WithWarning("Info", "Invalid data, cannot be saved.");
            }

            await Mediator.Send(new CreateArticleCommand {
                Title = model.Title,
                Description = model.Description,
                Tags = model.Tags,
                CategoryId = model.CategoryId,
                ProjectId = model.ProjectId,
                Content = model.Content,
                Seo = model.Seo,
                FileHeader = model.FileHeader,
                Files = model.Files,
                RemoteFileHeader = model.RemoteFileHeader,
                RemoteFileThumbnail = model.RemoteFileThumbnail,
                RemoteFiles = model.RemoteFiles,
            });
            
            return RedirectToAction("Index")
                .WithSuccess("Article", "Created successfully");
        }

        public async Task<IActionResult> Update(int id)
        {
            int projectId = HttpContext.Request.Cookies.GetProjectId().Value;
            var model = await Mediator.Send(new GetArticleDetailQuery { Id = id });

            model.Categories = await Mediator.Send(new GetCategoriesListQuery { ProjectId = model.ProjectId });
            model.ProjectId = projectId;

            return View(model);
        }

        /// <summary>
        /// Create Article.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Update(ArticleDetailViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Update", new { id = model.Id })
                    .WithWarning("Info", "Invalid data, cannot be updated.");
            }

            await Mediator.Send(new UpdateArticleCommand
            {
                Id = model.Id,
                Title = model.Title,
                Description = model.Description,
                Tags = model.Tags,
                CategoryId = model.CategoryId,
                ProjectId = model.ProjectId,
                Content = model.Content,
                Seo = model.Seo,
                FileHeader = model.FileHeader,
                Files = model.Files,
                RemoteFileHeader = model.RemoteFileHeader,
                RemoteFileThumbnail = model.RemoteFileThumbnail,
                RemoteFiles = model.RemoteFiles,
                ArticleSettings = model.ArticleSettings
            });
            
            return RedirectToAction("Index")
                .WithSuccess("Article", "Updated successfully");
        }
    }
}