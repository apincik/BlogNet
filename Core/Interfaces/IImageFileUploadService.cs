using Core.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IImageFileUploadService
    {
        Task<List<ImageFileDto>> UploadImages(int albumId, List<IFormFile> files);
        Task<List<ImageFileDto>> DownloadImages(int albumId, List<string> files);
    }
}
