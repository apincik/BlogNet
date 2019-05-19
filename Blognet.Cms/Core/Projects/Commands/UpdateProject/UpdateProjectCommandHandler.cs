using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Blognet.Cms.Core.Interfaces;
using Blognet.Cms.Domain.Exceptions;
using Blognet.Cms.Domain.Entities;
using AutoMapper;
using Blognet.Cms.Core.Model;
using Microsoft.EntityFrameworkCore;

namespace Blognet.Cms.Core.Projects.Commands.UpdateProject { 
    public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, Unit>
    {
        private IWebContext _context;

        public UpdateProjectCommandHandler(IWebContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Projects
                .Include(x => x.ProjectSettings)
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            if (entity == null)
            {
                throw new EntityNotFoundException(nameof(Project), request.Id);
            }

            if(entity.ProjectSettings == null)
            {
                entity.ProjectSettings = new ProjectSettings();
            }

            entity.Name = request.Name;
            entity.DomainName = request.DomainName;
            entity.Description = request.Description;

            Mapper.Map<ProjectSettingsDTO, ProjectSettings>(request.ProjectSettings, entity.ProjectSettings);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
