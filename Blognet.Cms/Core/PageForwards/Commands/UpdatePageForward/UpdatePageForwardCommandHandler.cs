using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Blognet.Cms.Core.Interfaces;
using Blognet.Cms.Domain.Exceptions;
using Blognet.Cms.Domain.Entities;

namespace Blognet.Cms.Core.PageForwards.Commands.UpdatePageForward { 
    public class UpdatePageForwardCommandHandler : IRequestHandler<UpdatePageForwardCommand, Unit>
    {
        private IWebContext _context;

        public UpdatePageForwardCommandHandler(IWebContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdatePageForwardCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.PageForwards.FindAsync(request.Id);
            if(entity == null)
            {
                throw new EntityNotFoundException(nameof(PageForward), request.Id);
            }

            entity.Mask = request.Mask;
            entity.SourceId = request.SourceId;
            entity.Type = request.Type;
            entity.DestinationUrl = request.DestinationUrl;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
