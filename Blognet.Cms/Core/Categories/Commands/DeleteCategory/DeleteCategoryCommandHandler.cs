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

namespace Blognet.Cms.Core.Categories.Commands.DeleteCategory
{
    public class DeleteAlbumCommandHandler : IRequestHandler<DeleteCategoryCommand, Unit>
    {
        private IWebContext _context;

        public DeleteAlbumCommandHandler(IWebContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Categories.FindAsync(request.Id);
            if(entity == null)
            {
                throw new EntityNotFoundException(nameof(Category), request.Id);
            }

            try
            {
                _context.Categories.Remove(entity);
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch(DbUpdateException)
            {
                throw new EntityDeleteException("Cannot delete, existing children.");
            }

            return Unit.Value;
        }
    }
}
