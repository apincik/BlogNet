using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Model
{
    public class ArticleImagesDto
    {
        public IFormFile FileThumbnail { get; set; }
        public IFormFile FileHeader { get; set; }
        public List<IFormFile> Files { get; set; }

        public string RemoteFileThumbnail { get; set; }
        public string RemoteFileHeader { get; set; }
        public string RemoteFiles { get; set; }
    }
}
