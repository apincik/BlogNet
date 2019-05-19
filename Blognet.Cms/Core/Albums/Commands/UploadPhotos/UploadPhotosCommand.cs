using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Blognet.Cms.Domain.Entities;
using Blognet.Cms.Domain.Enum;
using Microsoft.AspNetCore.Http;
using Blognet.Cms.Core.Model;

namespace Blognet.Cms.Core.Albums.Commands.UploadPhotos
{
    public class UploadPhotosCommand : IRequest<Unit>
    {
        public int AlbumId;
        public List<IFormFile> LocalFiles = new List<IFormFile>();
        public List<string> Files = new List<string>();
        public PhotoType PhotoType = PhotoType.Image;
    }
}
