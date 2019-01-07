using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Entities;
using Core.Enum;
using Core.Interfaces;
using Core.Interfaces.Repositories;
using Core.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Areas.Cms.ViewModels;
using Web.Areas.Cms.ViewModels.Articles;
using Web.Extensions;
using Web.Filters;

namespace Web.Areas.Cms.Controllers
{
    [Area("Cms")]
    public class ArticleController : Controller
    {
        private IArticleRepository _articleRepository;
        private ICategoryRepository _categoryRepository;
        private IArticleService _articleService;
        private ICategoryService _categoryService;
        private IProjectService _projectService;
        private IMapper _mapper;

        public ArticleController(
            IArticleRepository articleRepository,
            ICategoryRepository categoryRepository,
            IArticleService articleService,
            ICategoryService categoryService,
            IProjectService projectService,
            IMapper mapper)
        {
            _articleRepository = articleRepository;
            _categoryRepository = categoryRepository;
            _articleService = articleService;
            _categoryService = categoryService;
            _projectService = projectService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            int? projectId = HttpContext.Request.Cookies.GetProjectId();
            var articles = projectId != null ? await _articleRepository.ListAllByProjectId((int)projectId) : new List<Article>();
            return View(new ArticleViewModel
            {
                Articles = articles
            });
        }

        public async Task<IActionResult> ToggleStatus(int id)
        {
            await _articleService.ToggleStatusById(id);
            return RedirectToAction("Index");
        }

        [CheckProjectSet]
        public async Task<IActionResult> Create()
        {
            int projectId = HttpContext.Request.Cookies.GetProjectId().Value;
            var model = new ArticleCreateViewModel()
            {
                ProjectId = projectId,
                Categories = await _categoryRepository.ListAllByProjectId(projectId)
            };

            return View(model);
        }

        /// <summary>
        /// Create Article.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(ArticleCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("create").WithWarning("Info", "Cannot be saved.");
            }
            var article = _mapper.Map<Article>(model);
            await _articleService.Create(article, _mapper.Map(model, new ArticleImagesDto()));

            return RedirectToAction("Index").WithSuccess("Article", "Created successfuly");
        }

        public async Task<IActionResult> Update(int id)
        {
            var article = await _articleRepository.Get((int)id);
            var model = _mapper.Map<ArticleUpdateViewModel>(article);
            model.Categories = await _categoryRepository.ListAllByProjectId(article.ProjectId);

            return View(model);
        }

        /// <summary>
        /// Create Article.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Update(ArticleUpdateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("update", new { id = model.Id }).WithWarning("Info", "Cannot be updated");
            }

            var article = await _articleRepository.Get(model.Id);
            var articleModel = _mapper.Map(model, article);
            await _articleService.Update(articleModel, _mapper.Map<ArticleImagesDto>(model));

            return RedirectToAction("Index").WithSuccess("Article", "Updated successfuly");
        }
    }
}