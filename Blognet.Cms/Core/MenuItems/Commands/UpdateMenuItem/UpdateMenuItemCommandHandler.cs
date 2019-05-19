using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Blognet.Cms.Core.Interfaces;
using Blognet.Cms.Domain.Exceptions;
using Blognet.Cms.Domain.Entities;

namespace Blognet.Cms.Core.MenuItems.Commands.UpdateMenuItem { 
    public class UpdateMenuItemCommandHandler : IRequestHandler<UpdateMenuItemCommand, Unit>
    {
        private IWebContext _context;

        public UpdateMenuItemCommandHandler(IWebContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateMenuItemCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.MenuItems.FindAsync(request.Id);
            if(entity == null)
            {
                throw new EntityNotFoundException(nameof(MenuItem), request.Id);
            }

            entity.Title = request.Title;
            entity.ProjectId = request.ProjectId;
            entity.ParentMenuId = request.ParentMenuId;
            entity.Url = request.Url;
            
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
