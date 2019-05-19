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

namespace Blognet.Cms.Core.TemplateVariables.Commands.DeleteTemplateVariable
{
    public class DeleteAlbumCommandHandler : IRequestHandler<DeleteTemplateVariableCommand, Unit>
    {
        private IWebContext _context;

        public DeleteAlbumCommandHandler(IWebContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteTemplateVariableCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.TemplateVariables.FindAsync(request.Id);
            if(entity == null)
            {
                throw new EntityNotFoundException(nameof(TemplateVariable), request.Id);
            }

            try
            {
                _context.TemplateVariables.Remove(entity);
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
