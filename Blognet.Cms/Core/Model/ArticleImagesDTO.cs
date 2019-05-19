using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blognet.Cms.Core.Model
{
    public class ArticleImagesDTO
    {
        public IFormFile FileThumbnail { get; set; }

        public IFormFile FileHeader { get; set; }

        public List<IFormFile> Files { get; set; }

        public string RemoteFileThumbnail { get; set; }

        public string RemoteFileHeader { get; set; }

        public string RemoteFiles { get; set; }
    }
}
