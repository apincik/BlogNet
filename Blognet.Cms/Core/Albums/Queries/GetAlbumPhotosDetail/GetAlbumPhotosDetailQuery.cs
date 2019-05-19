using Blognet.Cms.Core.Albums.Models;
using Blognet.Cms.Core.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blognet.Cms.Core.Albums.Queries.GetAlbumPhotosDetail
{
    public class GetAlbumPhotosDetailQuery : IRequest<List<PhotoDTO>>
    {
        public int AlbumId;
    }
}
