using Blognet.Cms.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blognet.Cms.Core.Albums.Commands.TogglePhotoStatus
{
    public class TogglePhotoStatusCommand : IRequest<Unit>
    {
        public int Id;
    }
}
