using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Blognet.Cms.Domain.Enum;

namespace Blognet.Cms.Core.PageForwards.Commands.UpdatePageForward
{
    public class UpdatePageForwardCommand : IRequest<Unit>
    {
        public int Id { get; set; }

        public int ProjectId { get; set; }

        public string Mask { get; set; }

        public string DestinationUrl { get; set; }

        public string SourceId { get; set; }

        public PageForwardSourceType Type { get; set; }
    }
}
