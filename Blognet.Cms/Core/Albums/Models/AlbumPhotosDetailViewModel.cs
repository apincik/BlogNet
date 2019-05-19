using Blognet.Cms.Core.Model;
using Blognet.Cms.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blognet.Cms.Core.Albums.Models
{
    public class AlbumPhotosDetailViewModel
{
        public int AlbumId { get; set; }

        public List<PhotoDTO> Photos { get; set; }

        public List<IFormFile> Files { get; set; }
    }
}
