using Blognet.Cms.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blognet.Cms.Core.Albums.Commands.ToggleAlbumStatus
{
    public class ToggleAlbumStatusCommand : IRequest<Unit>
    {
        public int Id;
    }
}
