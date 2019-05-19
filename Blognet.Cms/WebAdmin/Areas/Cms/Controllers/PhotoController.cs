using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Blognet.Cms.WebAdmin.Controllers;
using Blognet.Cms.Core.Albums.Commands.TogglePhotoStatus;
using Blognet.Cms.Core.Albums.Models;
using Microsoft.AspNetCore.Http;
using Blognet.Cms.Core.Albums.Queries.GetAlbumPhotosDetail;
using Blognet.Cms.Core.Albums.Commands.UploadPhotos;

namespace Blognet.Cms.WebAdmin.Areas.Cms.Controllers
{
    [Area("Cms")]
    [Authorize(Roles = "admin")]
    public class PhotoController : BaseController
    {
        public async Task<IActionResult> ToggleStatus(int id, int albumId)
        {
            await Mediator.Send(new TogglePhotoStatusCommand { Id = id });

            return RedirectToAction("Album", new { id = albumId });
        }

        public async Task<IActionResult> Album(int id)
        {
            return View(new AlbumPhotosDetailViewModel
            {
                AlbumId = id,
                Files = new List<IFormFile>(),
                Photos = await Mediator.Send(new GetAlbumPhotosDetailQuery { AlbumId = id })
            });
        }

        [HttpPost]
        public async Task<IActionResult> Upload(AlbumPhotosDetailViewModel model)
        {
            await Mediator.Send(new UploadPhotosCommand
            {
                AlbumId = model.AlbumId,
                LocalFiles = model.Files
            });

            return RedirectToAction("Album", new { id = model.AlbumId });
        }
    }
}