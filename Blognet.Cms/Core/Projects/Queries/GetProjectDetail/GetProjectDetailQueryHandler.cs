using AutoMapper;
using Blognet.Cms.Core.Interfaces;
using Blognet.Cms.Core.MenuItems.Models;
using Blognet.Cms.Domain.Entities;
using Blognet.Cms.Domain.Exceptions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Blognet.Cms.Core.PageForwards.Models;
using Blognet.Cms.Core.Projects.Models;
using Microsoft.EntityFrameworkCore;

namespace Blognet.Cms.Core.Projects.Queries.GetProjectDetail
{
    public class GetProjectDetailQueryHandler : IRequestHandler<GetProjectDetailQuery, ProjectDetailViewModel>
    {
        private readonly IWebContext _context;
        private readonly IMapper _mapper;

        public GetProjectDetailQueryHandler(IWebContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProjectDetailViewModel> Handle(GetProjectDetailQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Projects
                .Include(x => x.ProjectSettings)
                .FirstOrDefaultAsync(x => x.Id == request.Id);
            
            if(entity == null)
            {
                throw new EntityNotFoundException(nameof(Project), request.Id.ToString());
            }

            return _mapper.Map<ProjectDetailViewModel>(entity);
        }
    }
}
