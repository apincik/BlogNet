using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Blognet.Cms.Core.Interfaces;
using Blognet.Cms.Domain.Entities;
using Blognet.Cms.Domain.Extensions;
using Blognet.Cms.Domain.Enum;
using Blognet.Cms.Core.Model;
using AutoMapper;
using System.Linq;

namespace Blognet.Cms.Core.Albums.Commands.UploadPhotos
{
    public class UploadPhotosCommandHandler : IRequestHandler<UploadPhotosCommand, Unit>
    {
        private IWebContext _context;
        private IMapper _mapper;
        private IImageFileUploadService _imageFileUploadService;

        public UploadPhotosCommandHandler(IWebContext context, IMapper mapper, IImageFileUploadService imageFileUploadService)
        {
            _context = context;
            _mapper = mapper;
            _imageFileUploadService = imageFileUploadService;
        }

        // @TODO Change return type.
        /// <summary>
        /// Handle upload local files and downloading remotes files with storing data into database.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Unit> Handle(UploadPhotosCommand request, CancellationToken cancellationToken)
        {
            List<ImageFileDTO> uploads = await _imageFileUploadService.DownloadImages(request.AlbumId, request.Files);
            List<ImageFileDTO> localUploads = await _imageFileUploadService.UploadImages(request.AlbumId, request.LocalFiles);
            uploads = uploads.Concat(localUploads).ToList();

            foreach (var upload in uploads)
            {
                Photo photo = _mapper.Map<Photo>(upload);
                photo.NameNormalized = photo.Name.GenerateSlug();
                photo.OriginSource = upload.OriginSource;
                photo.AlbumId = request.AlbumId;
                photo.Status = Status.Active;
                photo.Type = request.PhotoType;
                
                _context.Photos.Add(photo);
                await _context.SaveChangesAsync(cancellationToken);
                upload.Id = photo.Id;
            }

            return Unit.Value;
        }
    }
}
