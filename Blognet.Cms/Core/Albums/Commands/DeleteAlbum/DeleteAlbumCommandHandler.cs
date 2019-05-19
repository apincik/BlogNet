using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Blognet.Cms.Core.Interfaces;
using Blognet.Cms.Domain.Exceptions;
using Blognet.Cms.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blognet.Cms.Core.Albums.Commands.DeleteAlbum
{
    public class DeleteAlbumCommandHandler : IRequestHandler<DeleteAlbumCommand, Unit>
    {
        private IWebContext _context;

        public DeleteAlbumCommandHandler(IWebContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteAlbumCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Albums.FindAsync(request.Id);
            if(entity == null)
            {
                throw new EntityNotFoundException(nameof(Album), request.Id);
            }

            try
            {
                _context.Albums.Remove(entity);
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch(DbUpdateException)
            {
                throw new EntityDeleteException("Error occured during delete, check references.");
            }

            return Unit.Value;
        }
    }
}
