using AutoMapper;
using Core.Entities;
using Core.Enum;
using Core.Extensions;
using Core.Interfaces;
using Core.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class PhotoService : Service<Photo>, IPhotoService
    {
        private IMapper _mapper;
        private IHostingEnvironment _hostingEnvironment;
        private IImageFileUploadService _imageFileUploadService;

        public PhotoService(
            IAsyncModel<Photo> model,
            IMapper mapper,
            IHostingEnvironment hostingEnvironment,
            IImageFileUploadService imageFileUploadService) : base(model)
        {
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
            _imageFileUploadService = imageFileUploadService;
        }

        public async Task ToggleStatusById(int id)
        {
            Photo photo = await Repository.Table().FindAsync(id);
            photo.Status = photo.Status == Status.Inactive ? Status.Active : Status.Inactive;
            await Repository.UpdateAsync(photo);
        }

        /// <summary>
        /// Store local uploads from IFormFile
        /// </summary>
        /// <param name="albumId"></param>
        /// <param name="files"></param>
        /// <param name="photoType"></param>
        /// <returns></returns>
        public async Task<List<ImageFileDto>> UploadImages(int albumId, List<IFormFile> files, PhotoType photoType = PhotoType.Image)
        {
            List<ImageFileDto> uploads = await _imageFileUploadService.UploadImages(albumId, files);
            foreach(var upload in uploads)
            {
                Photo photo = _mapper.Map<Photo>(upload);
                photo.NameNormalized = photo.Name.GenerateSlug();
                photo.AlbumId = albumId;
                photo.Status = Status.Active;
                photo.Type = photoType;

                var storedPhoto = await Repository.AddAsync(photo);
                upload.Id = storedPhoto.Id;
            }

            return uploads;
        }

        /// <summary>
        /// Store remote files.
        /// </summary>
        /// <param name="albumId"></param>
        /// <param name="files"></param>
        /// <param name="photoType"></param>
        /// <returns></returns>
        public async Task<List<ImageFileDto>> UploadImages(int albumId, List<string> files, PhotoType photoType = PhotoType.Image)
        {
            List<ImageFileDto> uploads = await _imageFileUploadService.DownloadImages(albumId, files);
            foreach (var upload in uploads)
            {
                Photo photo = _mapper.Map<Photo>(upload);
                photo.NameNormalized = photo.Name.GenerateSlug();
                photo.AlbumId = albumId;
                photo.Status = Status.Active;
                photo.Type = photoType;

                var storedPhoto = await Repository.AddAsync(photo);
                upload.Id = storedPhoto.Id;
            }

            return uploads;
        }

        public Task<List<Photo>> ListAllByAlbumId(int albumId)
        {
            return Repository.Table()
                .Where(p => p.AlbumId == albumId)
                .ToListAsync();
        }
    }
}
