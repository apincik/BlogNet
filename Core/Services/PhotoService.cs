using AutoMapper;
using Core.Entities;
using Core.Enum;
using Core.Extensions;
using Core.Interfaces;
using Core.Interfaces.Repositories;
using Core.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class PhotoService : IPhotoService
    {
        private IPhotoRepository _photoRepository;
        private IMapper _mapper;
        private IHostingEnvironment _hostingEnvironment;
        private IImageFileUploadService _imageFileUploadService;

        public PhotoService(
            IPhotoRepository photoRepository,
            IMapper mapper,
            IHostingEnvironment hostingEnvironment,
            IImageFileUploadService imageFileUploadService)
        {
            _photoRepository = photoRepository;
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
            _imageFileUploadService = imageFileUploadService;
        }

        public async Task ToggleStatusById(int id)
        {
            Photo photo = await _photoRepository.Get(id);
            photo.Status = photo.Status == Status.Inactive ? Status.Active : Status.Inactive;
            await _photoRepository.Update(photo);
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

                var storedPhoto = await _photoRepository.Add(photo);
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

                var storedPhoto = await _photoRepository.Add(photo);
                upload.Id = storedPhoto.Id;
            }

            return uploads;
        }
    }
}
