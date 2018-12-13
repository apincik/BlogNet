using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Web.Areas.Account.ViewModels.Projects;
using Web.Extensions;

namespace Web.Areas.Account.Controllers
{
    [Authorize]
    [Area("Account")]
    public class ProjectController : Controller
    {
        private IProjectService _projectService;
        private UserManager<ApplicationUser> _userManager;
        private IMapper _mapper;

        public ProjectController(
            IProjectService projectService,
            UserManager<ApplicationUser> userManager,
            IMapper mapper)
        {
            _projectService = projectService;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var projects = await _projectService.ListAll();
            return View(new ProjectViewModel {
                Projects = projects
            });
        }

        public IActionResult Create()
        {
            return View(new ProjectCreateViewModel());
        }

        /// <summary>
        /// Create or update Project.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(ProjectCreateViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View("create", model);
            }

            var project = _mapper.Map<Project>(model);
            project.UserId = _userManager.GetUserId(User);
            await _projectService.Create(project);

            return RedirectToAction("Index").WithSuccess("Project", "Created successfuly");
        }

        public async Task<IActionResult> Update(int id)
        {
            var project = await _projectService.Get(id);
            var model = _mapper.Map<ProjectUpdateViewModel>(project);

            return View(model);
        }

        /// <summary>
        /// Create or update Project.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Update(ProjectUpdateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("update", model);
            }

            var project = await _projectService.Get(model.Id);
            var projectMap = _mapper.Map(model, project);
            await _projectService.Update(projectMap);

            return RedirectToAction("Index").WithSuccess("Article", "Updated successfuly");
        }

        /// <summary>
        /// Store selected project ID to session.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Select(int id)
        {
            if(HttpContext.Request.Cookies.GetProjectId() == id)
            {
                HttpContext.Response.Cookies.SetProjectId(null);
            } 
            else
            {
                HttpContext.Response.Cookies.SetProjectId(id);
            }
            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}