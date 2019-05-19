using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Blognet.Cms.Core.Interfaces;
using Blognet.Cms.Domain.Entities;
using Blognet.Cms.Domain.Exceptions;
using Blognet.Cms.Domain.Extensions;
using Blognet.Cms.Domain.Enum;

namespace Blognet.Cms.Core.Albums.Commands.ToggleAlbumStatus
{ 
    public class ToggleAlbumStatusCommandHandler : IRequestHandler<ToggleAlbumStatusCommand, Unit>
    {
        private IWebContext _context;

        public ToggleAlbumStatusCommandHandler(IWebContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(ToggleAlbumStatusCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Albums.FindAsync(request.Id);
            if(entity == null)
            {
                throw new EntityNotFoundException(nameof(Album), request.Id);
            }
            
            entity.Status = entity.Status == Status.Inactive ? Status.Active : Status.Inactive;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
