﻿using Blognet.Cms.Core.Interfaces;
using Blognet.Cms.Core.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Blognet.Cms.Infrastructure.Services
{
    public class ImageFileUploadService : IImageFileUploadService
    {
        IHostingEnvironment _hostingEnvironment;
        IConfiguration _configuration;

        public ImageFileUploadService(
            IHostingEnvironment hostingEnvironment,
            IConfiguration configuration)
        {
            _hostingEnvironment = hostingEnvironment;
            _configuration = configuration;
        }

        private bool IsExtensionAllowed(string fileExtension)
        {
            //IConfigurationSection section = _configuration.GetSection("ImageUploads:AllowedFormats");
            string[] allowedFormats = _configuration.GetSection("ImageUploads:AllowedFormats").Get<string[]>();
            foreach (var format in allowedFormats)
            {
                // @TODO could check end of string instead, update for remote files...
                if(fileExtension.Contains(format))
                {
                    return true;
                }
            }

            return false;
        }

        private string GetImageExtensionFromString(string str)
        {
            str = str.ToLower();
            if (str.Contains(@".gif"))
                return ".gif";
            else if (str.Contains(@".jpg") || str.Contains(@".jpeg"))
                return ".jpg";
            else if (str.Contains(@".png"))
                return ".png";
            else
                return null;
        }

        // Sha256 
        // @TODO In case of need, move to separate class.
        private string ComputeHash(byte[] data)
        {
            var crypt = new System.Security.Cryptography.SHA256Managed();
            var hash = new System.Text.StringBuilder();
            byte[] crypto = crypt.ComputeHash(data);
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }

            return hash.ToString();
        }

        public async Task<List<ImageFileDTO>> UploadImages(int albumId, List<IFormFile> files)
        {
            List<ImageFileDTO> uploadedFiles = new List<ImageFileDTO>();

            foreach (IFormFile file in files)
            {
                if(!IsExtensionAllowed(Path.GetExtension(file.FileName)))
                {
                    break;
                }

                // Guid used temporary instead computing hash from attributes
                var fileGuid = Guid.NewGuid().ToString();
                var extension = Path.GetExtension(file.FileName);
                var uniqueFileName = fileGuid + extension;
                var uploads = Path.Combine(_hostingEnvironment.WebRootPath, _configuration["ImageUploads:LocalRelativePath"]);
                var albumDir = Path.Combine(uploads, albumId.ToString());

                // Create album dir if not exists.
                System.IO.Directory.CreateDirectory(albumDir);

                var filePath = Path.Combine(albumDir, uniqueFileName);
                
                using (FileStream fs = File.Create(filePath))
                {
                    await file.CopyToAsync(fs);
                }

                Image image = Image.FromFile(filePath);
                byte[] imageData = await File.ReadAllBytesAsync(filePath);
                string imageHash = ComputeHash(imageData);

                uploadedFiles.Add(new ImageFileDTO
                {
                    Hash = fileGuid,
                    ImageHash = imageHash,
                    BasePath = _hostingEnvironment.WebRootPath,
                    RelativePath = _configuration["ImageUploads:LocalRelativePath"],
                    Name = file.FileName.Replace(extension, ""),
                    Width = image.Width,
                    Height = image.Height,
                    IsLocal = true,
                    Extension = extension.Replace(".", ""),
                });
            }

            return uploadedFiles;
        }

        #pragma warning disable CS0168
        public async Task<List<ImageFileDTO>> DownloadImages(int albumId, List<string> files)
        {
            List<ImageFileDTO> uploadedFiles = new List<ImageFileDTO>();

            foreach(var filelink in files)
            {
                var extension = GetImageExtensionFromString(filelink);
                if (extension == null || !IsExtensionAllowed(extension))
                {
                    break;
                }

                // Guid used temporary instead computing hash from attributes
                var filename = filelink.Split("/").LastOrDefault();
                var fileGuid = Guid.NewGuid().ToString();
                var uniqueFileName = fileGuid + extension;
                var uploads = Path.Combine(_hostingEnvironment.WebRootPath, _configuration["ImageUploads:LocalRelativePath"]);
                var albumDir = Path.Combine(uploads, albumId.ToString());

                // Create album dir if not exists.
                System.IO.Directory.CreateDirectory(albumDir);

                var filePath = Path.Combine(albumDir, uniqueFileName);
                try
                {
                    using (WebClient client = new WebClient())
                    {
                        await client.DownloadFileTaskAsync(new Uri(filelink), filePath);
                    }
                }
                catch(WebException e)
                {
                    continue;
                }

                Image image = Image.FromFile(filePath);
                byte[] imageData = await File.ReadAllBytesAsync(filePath);
                string imageHash = ComputeHash(imageData);

                uploadedFiles.Add(new ImageFileDTO
                {
                    Hash = fileGuid,
                    ImageHash = imageHash,
                    BasePath = _hostingEnvironment.WebRootPath,
                    RelativePath = _configuration["ImageUploads:LocalRelativePath"],
                    Name = filename,
                    Width = image.Width,
                    Height = image.Height,
                    IsLocal = true,
                    Extension = extension.Replace(".", ""),
                    OriginSource = filelink
                });
            }

            return uploadedFiles;
        }
    }
}
