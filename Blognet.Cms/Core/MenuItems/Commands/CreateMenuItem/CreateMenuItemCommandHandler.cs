using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Blognet.Cms.Core.Interfaces;
using Blognet.Cms.Domain.Entities;

namespace Blognet.Cms.Core.MenuItems.Commands.CreateMenuItem
{
    public class CreateMenuItemCommandHandler : IRequestHandler<CreateMenuItemCommand, int>
    {
        private IWebContext _context;

        public CreateMenuItemCommandHandler(IWebContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateMenuItemCommand request, CancellationToken cancellationToken)
        {
            var entity = new MenuItem
            {
                Title = request.Title,
                ProjectId = request.ProjectId,
                ParentMenuId = request.ParentMenuId,
                Url = request.Url
            };

            _context.MenuItems.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
