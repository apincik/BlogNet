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
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Areas.Cms.ViewModels;
using Web.Areas.Cms.ViewModels.Categories;
using Web.Areas.Web.ViewModels.Variables;
using Web.Extensions;
using Web.Filters;

namespace Web.Areas.Cms.Controllers
{
    [Area("Web")]
    public class VariableController : Controller
    {
        private IMapper _mapper;
        private ITemplateVariableRepository _templateVariableRepository;
        private ITemplateVariableService _templateVariableService;

        public VariableController(
            IMapper mapper,
            ITemplateVariableRepository templateVariableRepository,
            ITemplateVariableService templateVariableService)
        {
            _mapper = mapper;
            _templateVariableRepository = templateVariableRepository;
            _templateVariableService = templateVariableService;
        }
        
        public async Task<IActionResult> Index()
        {
            int? projectId = HttpContext.Request.Cookies.GetProjectId();
            var variables = projectId != null ? await _templateVariableRepository.ListAllByProjectId((int)projectId) : new List<TemplateVariable>();
            return View(new VariableViewModel
            {
                Variables = variables
            });
        }

        public async Task<IActionResult> ToggleStatus(int id)
        {
            await _templateVariableService.ToggleStatusById(id);
            return RedirectToAction("Index");
        }

        [CheckProjectSet]
        public async Task<IActionResult> Create()
        {
            var model = new VariableCreateViewModel();
            int projectId = HttpContext.Request.Cookies.GetProjectId().Value;
            model.ProjectId = projectId;
            model.Variables = await _templateVariableRepository.ListAllByProjectId(projectId);

            return View(model);
        }

        /// <summary>
        /// Create category.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(VariableCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                int projectId = HttpContext.Request.Cookies.GetProjectId().Value;
                model.ProjectId = projectId;
                model.Variables = await _templateVariableRepository.ListAllByProjectId(projectId);
                return View("create", model).WithWarning("Info", "Error occured, cannot be saved.");
            }

            var variable = _mapper.Map<TemplateVariable>(model);
            await _templateVariableService.Create(variable);

            return RedirectToAction("Index").WithSuccess("Variable", "Created successfuly.");
        }

        [CheckProjectSet]
        public async Task<IActionResult> Update(int id)
        {
            var variable = await _templateVariableRepository.Get(id);
            var model = _mapper.Map<VariableUpdateViewModel>(variable);
            model.Variables = await _templateVariableRepository.ListAllByProjectId(model.ProjectId);
            model.Variables = model.Variables.Where(c => c.Id != model.Id).ToList();

            return View(model);
        }

        /// <summary>
        /// Create category.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Update(VariableUpdateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Update", new { id = model.Id }).WithWarning("Invalid data.", "Cannot be saved.");
            }

            var variable = _mapper.Map<TemplateVariable>(model);
            await _templateVariableService.Update(variable);

            return RedirectToAction("Index").WithSuccess("Variable", "Updated successfuly.");
        }

    }
}