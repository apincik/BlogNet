using Blognet.Cms.Domain.Entities;
using Blognet.Cms.Domain.Enum;
using Blognet.Cms.Core.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blognet.Cms.Core.Interfaces
{
    public interface IPhotoService
    {
        Task<List<ImageFileDTO>> UploadImages(int albumId, List<IFormFile> files, PhotoType photoType = PhotoType.Image);

        Task<List<ImageFileDTO>> UploadImages(int albumId, List<string> files, PhotoType photoType = PhotoType.Image);
        //Task<List<ImageFileDTO>> DownloadFiles(int albumId, List<string> files, PhotoType photoType = PhotoType.Image);

        Task ToggleStatusById(int id);
    }
}
