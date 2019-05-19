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

namespace Blognet.Cms.Core.Albums.Commands.UpdateAlbum { 
    public class UpdateAlbumCommandHandler : IRequestHandler<UpdateAlbumCommand, Unit>
    {
        private IWebContext _context;

        public UpdateAlbumCommandHandler(IWebContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateAlbumCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Albums.FindAsync(request.Id);
            if(entity == null)
            {
                throw new EntityNotFoundException(nameof(Album), request.Id);
            }

            entity.NameNormalized = request.Name.GenerateSlug();

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
