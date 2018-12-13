using Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Areas.Cms.ViewModels
{
    public class PhotoAlbumViewModel
    {
        public int AlbumId { get; set; }

        public List<Photo> Photos { get; set; }

        public List<IFormFile> Files { get; set; }
    }
}
