using Blognet.Cms.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blognet.Cms.Core.Categories.Commands.ToggleCategoryStatus
{
    public class ToggleCategoryStatusCommand : IRequest
    {
        public int Id;
    }
}
