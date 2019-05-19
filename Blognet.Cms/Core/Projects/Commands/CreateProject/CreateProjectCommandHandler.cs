using Blognet.Cms.Core.Interfaces;
using Blognet.Cms.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Blognet.Cms.Core.Projects.Commands.CreateProject
{
    public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, int>
    {
        private IWebContext _context;

        public CreateProjectCommandHandler(IWebContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            var entity = new Project
            {
                Name = request.Name,
                DomainName = request.DomainName,
                UserId = request.UserId
            };

            _context.Projects.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
