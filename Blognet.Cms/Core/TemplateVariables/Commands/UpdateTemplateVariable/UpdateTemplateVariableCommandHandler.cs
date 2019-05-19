using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Blognet.Cms.Core.Interfaces;
using Blognet.Cms.Domain.Entities;
using Blognet.Cms.Domain.Exceptions;

namespace Blognet.Cms.Core.TemplateVariables.Commands.UpdateTemplateVariable
{ 
    public class UpdateTemplateVariableCommandHandler : IRequestHandler<UpdateTemplateVariableCommand, Unit>
    {
        private IWebContext _context;

        public UpdateTemplateVariableCommandHandler(IWebContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateTemplateVariableCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.TemplateVariables.FindAsync(request.Id);
            if (entity == null)
            {
                throw new EntityNotFoundException(nameof(MenuItem), request.Id);
            }

            entity.Label = request.Label;
            entity.Content = request.Content;
            entity.Type = request.Type;
            entity.ShowRaw = request.ShowRaw;
            entity.ParentTemplateVariableId = request.ParentTemplateVariableId;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
