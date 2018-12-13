using System.Threading.Tasks;
using AutoMapper;
using Core.Entities;
using Core.Enum;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Web.Areas.Cms.ViewModels.Albums;
using Web.Extensions;

namespace Web.Areas.Cms.Controllers
{
    [Area("Cms")]
    public class AlbumController : Controller
    {
        private IAlbumService _albumService;
        private IMapper _mapper;

        public AlbumController(
            IAlbumService albumService,
            IMapper mapper)
        {
            _albumService = albumService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var albums = await _albumService.ListAll();
            return View(new AlbumViewModel()
            {
                Albums = albums
            });
        }

        public async Task<IActionResult> ToggleStatus(int id)
        {
            await _albumService.ToggleStatusById(id);
            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Create or update Project.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(AlbumCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", model).WithInfo("Invalid data", "Cannot be saved.");
            }

            var album = _mapper.Map<Album>(model);
            await _albumService.Create(album);

            return RedirectToAction("Index").WithSuccess("Album", "Created successfuly");
        }

        public async Task<IActionResult> Update(int id)
        {
            var album = await _albumService.Get((int)id);
            var model = _mapper.Map<AlbumUpdateViewModel>(album);

            return View(model);
        }

        /// <summary>
        /// Create or update Project.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Update(AlbumUpdateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Update", model.Id).WithWarning("Invalid model.", "Cannot be saved.");
            }

            var album = _mapper.Map<Album>(model);
            await _albumService.Update(album);

            return RedirectToAction("Index").WithSuccess("Album", "Updated successfuly");
        }
    }
}