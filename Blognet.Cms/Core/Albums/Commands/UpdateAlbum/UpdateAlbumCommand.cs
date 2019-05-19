using Blognet.Cms.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blognet.Cms.Core.Albums.Commands.UpdateAlbum
{
    public class UpdateAlbumCommand : IRequest<Unit>
    {
        public int Id;
        public string Name;
    }
}
