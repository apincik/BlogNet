using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blognet.Cms.Core.PageForwards.Commands.DeletePageForward
{
    public class DeletePageForwardCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
