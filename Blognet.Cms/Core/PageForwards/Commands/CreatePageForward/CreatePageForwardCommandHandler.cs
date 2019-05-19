using Blognet.Cms.Core.Interfaces;
using Blognet.Cms.Domain.Entities;
using Blognet.Cms.Domain.Enum;
using Blognet.Cms.Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Blognet.Cms.Core.PageForwards.Commands.CreatePageForward
{
    public class CreatePageForwardCommandHandler : IRequestHandler<CreatePageForwardCommand, int>
    {
        private IWebContext _context;

        public CreatePageForwardCommandHandler(IWebContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreatePageForwardCommand request, CancellationToken cancellationToken)
        {
            var entity = new PageForward
            {
                Mask = request.Mask,
                ProjectId = request.ProjectId,
                SourceId = request.SourceId,
                Type = request.Type,
                DestinationUrl = request.DestinationUrl
            };

            _context.PageForwards.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
