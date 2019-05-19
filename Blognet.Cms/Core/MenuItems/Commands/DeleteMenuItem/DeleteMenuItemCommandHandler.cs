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

namespace Blognet.Cms.Core.MenuItems.Commands.DeleteMenuItem
{
    public class DeleteMenuItemCommandHandler : IRequestHandler<DeleteMenuItemCommand, Unit>
    {
        private IWebContext _context;

        public DeleteMenuItemCommandHandler(IWebContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteMenuItemCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.MenuItems.FindAsync(request.Id);
            if(entity == null)
            {
                throw new EntityNotFoundException(nameof(MenuItem), request.Id);
            }

            try
            {
                _context.MenuItems.Remove(entity);
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch(DbUpdateException)
            {
                throw new EntityDeleteException("Item has existing children.");
            }

            return Unit.Value;
        }
    }
}
