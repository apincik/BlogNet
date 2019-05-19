using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Blognet.Cms.Domain.Entities;
using Blognet.Cms.Domain.Enum;

namespace Blognet.Cms.Core.Albums.Commands.CreateAlbum
{
    public class CreateAlbumCommand : IRequest<int>
    {
        public string Name;
        public AlbumType? AlbumType;
        public Status? Status;
    }
}
