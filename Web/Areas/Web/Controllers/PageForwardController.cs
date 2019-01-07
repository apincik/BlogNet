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
using Web.Areas.Web.ViewModels.PageForwards;
using Web.Extensions;
using Web.Filters;

namespace Web.Areas.Cms.Controllers
{
    [Area("Web")]
    public class PageForwardController : Controller
    {
        private IMapper _mapper;
        public IPageForwardRepository _pageForwardRepository;
        private IPageForwardService _pageForwardService;

        public PageForwardController(
            IMapper mapper,
            IPageForwardRepository pageForwardRepository,
            IPageForwardService pageForwardService)
        {
            _mapper = mapper;
            _pageForwardRepository = pageForwardRepository;
            _pageForwardService = pageForwardService;
        }
        
        public async Task<IActionResult> Index()
        {
            int? projectId = HttpContext.Request.Cookies.GetProjectId();
            var forwards = projectId != null ? await _pageForwardRepository.ListAllByProjectId((int)projectId) : new List<PageForward>();
            return View(new PageForwardViewModel
            {
                Forwards = forwards
            });
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _pageForwardService.DeleteById(id);
            return RedirectToAction("Index");
        }

        [CheckProjectSet]
        public IActionResult Create()
        {
            return View(new PageForwardCreateViewModel()
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
        public async Task<IActionResult> Create(PageForwardCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.ProjectId = HttpContext.Request.Cookies.GetProjectId().Value;
                return View("create", model).WithWarning("Info", "Validation error, cannot be saved.");
            }

            var forward = _mapper.Map<PageForward>(model);
            await _pageForwardService.Create(forward);

            return RedirectToAction("Index").WithSuccess("Page forward", "Created successfuly.");
        }

        [CheckProjectSet]
        public async Task<IActionResult> Update(int id)
        {
            var forward = await _pageForwardRepository.Get(id);
            var model = _mapper.Map<PageForwardUpdateViewModel>(forward);

            return View(model);
        }

        /// <summary>
        /// Create category.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Update(PageForwardUpdateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Update", new { id = model.Id }).WithWarning("Invalid data.", "Cannot be saved.");
            }
            var forward = _mapper.Map<PageForward>(model);
            await _pageForwardService.Update(forward);

            return RedirectToAction("Index").WithSuccess("Page forward", "Updated successfuly.");
        }

    }
}