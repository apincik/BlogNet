using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blognet.Cms.Core.Albums.Commands.DeleteAlbum
{
    public class DeleteAlbumCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
