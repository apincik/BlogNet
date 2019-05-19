using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Blognet.Cms.Core.Interfaces;
using Blognet.Cms.Domain.Exceptions;
using Blognet.Cms.Domain.Entities;

namespace Blognet.Cms.Core.PageForwards.Commands.DeletePageForward
{
    public class DeletePageForwardCommandHandler : IRequestHandler<DeletePageForwardCommand, Unit>
    {
        private IWebContext _context;

        public DeletePageForwardCommandHandler(IWebContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeletePageForwardCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.PageForwards.FindAsync(request.Id);
            if(entity == null)
            {
                throw new EntityNotFoundException(nameof(MenuItem), request.Id);
            }

            _context.PageForwards.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
