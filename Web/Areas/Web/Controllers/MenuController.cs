using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Entities;
using Core.Enum;
using Core.Interfaces;
using Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using Web.Areas.Web.ViewModels.MenuItems;
using Web.Extensions;
using Web.Filters;

namespace Web.Areas.Cms.Controllers
{
    [Area("Web")]
    public class MenuController : Controller
    {
        private IMapper _mapper;
        private IMenuItemRepository _menuItemRepository;
        private IMenuItemService _menuItemService;

        public MenuController(
            IMapper mapper,
            IMenuItemRepository menuItemRepository,
            IMenuItemService menuItemService)
        {
            _mapper = mapper;
            _menuItemRepository = menuItemRepository;
            _menuItemService = menuItemService;
        }
        
        public async Task<IActionResult> Index()
        {
            int? projectId = HttpContext.Request.Cookies.GetProjectId();
            var items = projectId != null ? await _menuItemRepository.ListAllByProjectId((int)projectId) : new List<MenuItem>();
            return View(new MenuItemViewModel
            {
                Items = items
            });
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _menuItemService.Delete(id);
            return RedirectToAction("Index");
        }

        [CheckProjectSet]
        public async Task<IActionResult> Create()
        {
            int projectId = HttpContext.Request.Cookies.GetProjectId().Value;
            var model = new MenuItemCreateViewModel()
            {
                ProjectId = projectId,
                Items = await _menuItemRepository.ListAllByProjectId(projectId)
            };

            return View(model);
        }

        /// <summary>
        /// Create category.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(MenuItemCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                int projectId = HttpContext.Request.Cookies.GetProjectId().Value;
                model.ProjectId = projectId;
                model.Items = await _menuItemRepository.ListAllByProjectId(projectId);
                return View("create", model);
            }

            var item = _mapper.Map<MenuItem>(model);
            await _menuItemService.Create(item);

            return RedirectToAction("Index").WithSuccess("Menu", "Created successfuly");
        }

        [CheckProjectSet]
        public async Task<IActionResult> Update(int id)
        {
            var variable = await _menuItemRepository.Get((int)id);
            var model = _mapper.Map<MenuItemUpdateViewModel>(variable);
            model.Items = await _menuItemRepository.ListAllByProjectId(model.ProjectId);
            model.Items = model.Items.Where(c => c.Id != model.Id).ToList();

            return View(model);
        }

        /// <summary>
        /// Create category.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Update(MenuItemUpdateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Update", new { id = model.Id }).WithWarning("Invalid data.", "Cannot be saved.");
            }

            var variable = _mapper.Map<MenuItem>(model);
            await _menuItemService.Update(variable);

            return RedirectToAction("Index").WithSuccess("Menu", "Updated successfuly");
        }

    }
}