using Blognet.Cms.Core.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blognet.Cms.Core.Interfaces
{
    public interface IImageFileUploadService
    {
        Task<List<ImageFileDTO>> UploadImages(int albumId, List<IFormFile> files);
        Task<List<ImageFileDTO>> DownloadImages(int albumId, List<string> files);
    }
}
