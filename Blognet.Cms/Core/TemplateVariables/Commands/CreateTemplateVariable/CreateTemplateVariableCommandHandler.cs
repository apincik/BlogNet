using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Blognet.Cms.Core.Interfaces;
using Blognet.Cms.Domain.Entities;

namespace Blognet.Cms.Core.TemplateVariables.Commands.CreateTemplateVariable
{
    public class CreateTemplateVariableCommandHandler : IRequestHandler<CreateTemplateVariableCommand, int>
    {
        private IWebContext _context;

        public CreateTemplateVariableCommandHandler(IWebContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateTemplateVariableCommand request, CancellationToken cancellationToken)
        {
            var entity = new TemplateVariable
            {
                ProjectId = request.ProjectId,
                Label = request.Label,
                Content = request.Content,
                Type = request.Type,
                ShowRaw = request.ShowRaw,
                ParentTemplateVariableId = request.ParentTemplateVariableId
            };

            _context.TemplateVariables.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
