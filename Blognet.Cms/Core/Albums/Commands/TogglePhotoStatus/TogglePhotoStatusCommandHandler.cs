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

namespace Blognet.Cms.Core.Albums.Commands.TogglePhotoStatus
{ 
    public class TogglePhotoStatusCommandHandler : IRequestHandler<TogglePhotoStatusCommand, Unit>
    {
        private IWebContext _context;

        public TogglePhotoStatusCommandHandler(IWebContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(TogglePhotoStatusCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Photos.FindAsync(request.Id);
            if(entity == null)
            {
                throw new EntityNotFoundException(nameof(Photo), request.Id);
            }
            
            entity.Status = entity.Status == Status.Inactive ? Status.Active : Status.Inactive;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
