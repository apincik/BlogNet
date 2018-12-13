using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Entities;
using Core.Enum;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Areas.Cms.ViewModels;
using Web.Areas.Cms.ViewModels.Categories;
using Web.Extensions;
using Web.Filters;

namespace Web.Areas.Cms.Controllers
{
    [Area("Cms")]
    public class CategoryController : Controller
    {
        private ICategoryService _categoryService;
        private IProjectService _projectService;
        private IMapper _mapper;

        public CategoryController(
            ICategoryService categoryService,
            IProjectService projectService,
            IMapper mapper)
        {
            _categoryService = categoryService;
            _projectService = projectService;
            _mapper = mapper;
        }

        /// <summary>
        /// Listing categories by set project.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            int? projectId = HttpContext.Request.Cookies.GetProjectId();
            var categories = projectId != null ? await _categoryService.ListAllByProjectId((int)projectId) : new List<Category>();
            return View(new CategoryViewModel
            {
                Categories = categories
            });
        }

        /// <summary>
        /// Update category status.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> ToggleStatus(int id)
        {
            await _categoryService.ToggleStatusById(id);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Create form page for category.
        /// </summary>
        /// <returns></returns>
        [CheckProjectSet]
        public async Task<IActionResult> Create()
        {
            int projectId = HttpContext.Request.Cookies.GetProjectId().Value;
            var model = new CategoryCreateViewModel()
            {
                ProjectId = projectId,
                Categories = await _categoryService.ListAllByProjectId(projectId)
            };
            
            return View(model);
        }

        /// <summary>
        /// Create category.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(CategoryCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Create").WithWarning("Invalid data.", "Cannot be saved.");
            }

            var project = _mapper.Map<Category>(model);
            await _categoryService.Create(project);

            return RedirectToAction("Index").WithSuccess("Category", "Created successfuly");
        }

        /// <summary>
        /// Update form page.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [CheckProjectSet]
        public async Task<IActionResult> Update(int id)
        {
            var category = await _categoryService.GetWithSeo((int)id);
            var model = _mapper.Map<CategoryUpdateViewModel>(category);

            var categories = await _categoryService.ListAllByProjectId(model.ProjectId);
            model.Categories = categories.Where(c => c.Id != model.Id).ToList();

            return View(model);
        }

        /// <summary>
        /// Create category.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Update(CategoryUpdateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Update", new { id = model.Id }).WithWarning("Invalid data.", "Cannot be saved.");
            }

            var project = _mapper.Map<Category>(model);
            await _categoryService.Update(project);

            return RedirectToAction("Index").WithSuccess("Category", "Updated successfuly");
        }

    }
}