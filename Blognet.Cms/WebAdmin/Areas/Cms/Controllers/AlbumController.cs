using System.Threading.Tasks;
using Blognet.Cms.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Blognet.Cms.WebAdmin.Extensions;
using Microsoft.AspNetCore.Authorization;
using Blognet.Cms.Core.Albums.Commands.UpdateAlbum;
using Blognet.Cms.WebAdmin.Controllers;
using Blognet.Cms.Core.Albums.Commands.CreateAlbum;
using Blognet.Cms.Core.Albums.Models;
using Domain.Enum;
using Blognet.Cms.Core.Model;
using System.Collections.Generic;
using Blognet.Cms.Core.Albums.Commands.ToggleAlbumStatus;
using Blognet.Cms.Core.Albums.Queries.GetAlbumsList;
using Blognet.Cms.Core.Albums.Queries.GetAlbumDetail;
using Blognet.Cms.Core.Albums.Commands.DeleteAlbum;
using Blognet.Cms.Domain.Exceptions;

namespace Blognet.Cms.WebAdmin.Areas.Cms.Controllers
{
    [Area("Cms")]
    [Authorize(Roles = "admin")]
    public class AlbumController : BaseController
    {
        public async Task<IActionResult> Index()
        {
            List<AlbumDTO> albums = await Mediator.Send(new GetAlbumsListQuery
            {
                Page = 0,
                Limit = 10,
                Order = Order.DESC,
                OrderByColumnName = nameof(Album.Id)
            });
            return View(
                albums
            );
        }

        /// <summary>
        /// Toggle album status.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> ToggleStatus(int id)
        {
            await Mediator.Send(new ToggleAlbumStatusCommand { Id = id });
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await Mediator.Send(new DeleteAlbumCommand { Id = id });
            }
            catch (EntityDeleteException e)
            {
                return RedirectToAction("Index")
                    .WithWarning("Album", e.Message);
            }

            return RedirectToAction("Index")
                .WithSuccess("Album", "Deleted succesfuly.");
        }

        /// <summary>
        /// Album create page.
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            return View(new AlbumDetailViewModel());
        }

        /// <summary>
        /// Create album.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromForm]AlbumDetailViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", model)
                    .WithInfo("Invalid data", "Cannot be saved.");
            }
            
            await Mediator.Send(new CreateAlbumCommand { Name = model.Name });
            
            return RedirectToAction("Index")
                .WithSuccess("Album", "Created successfully");
        }

        /// <summary>
        /// Album update page.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Update(int id)
        {
            var model = await Mediator.Send(new GetAlbumDetailQuery { Id = id});
            return View(model);
        }

        /// <summary>
        /// Update album.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Update(AlbumDetailViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Update", model.Id)
                    .WithWarning("Invalid model.", "Cannot be saved.");
            }

            await Mediator.Send(new UpdateAlbumCommand { Name = model.Name, Id = model.Id.Value  });

            return RedirectToAction("Index")
                .WithSuccess("Album", "Updated successfully");
        }
    }
}