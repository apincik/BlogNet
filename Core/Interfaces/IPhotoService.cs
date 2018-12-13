using Core.Entities;
using Core.Enum;
using Core.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IPhotoService : IService<Photo>
    {
        Task<List<ImageFileDto>> UploadImages(int albumId, List<IFormFile> files, PhotoType photoType = PhotoType.Image);

        Task<List<ImageFileDto>> UploadImages(int albumId, List<string> files, PhotoType photoType = PhotoType.Image);
        //Task<List<ImageFileDto>> DownloadFiles(int albumId, List<string> files, PhotoType photoType = PhotoType.Image);

        Task<List<Photo>> ListAllByAlbumId(int albumId);

        Task ToggleStatusById(int id);
    }
}
