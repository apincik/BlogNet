using Blognet.Cms.Core.Albums.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blognet.Cms.Core.Albums.Queries.GetAlbumDetail
{
    public class GetAlbumDetailQuery : IRequest<AlbumDetailViewModel>
    {
        public int Id;
    }
}
