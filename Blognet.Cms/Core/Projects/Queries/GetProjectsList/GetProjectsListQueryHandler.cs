using AutoMapper;
using Blognet.Cms.Core.Interfaces;
using Blognet.Cms.Core.Model;
using Domain.Enum;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Blognet.Cms.Core.Extensions;

namespace Blognet.Cms.Core.PageForwards.Queries.GetProjectsList
{
    public class GetProjectsListQueryHandler : IRequestHandler<GetProjectsListQuery, List<ProjectDTO>>
    {
        private readonly IWebContext _context;
        private readonly IMapper _mapper;

        public GetProjectsListQueryHandler(IWebContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ProjectDTO>> Handle(GetProjectsListQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Projects
                .AsQueryable();

            if(!String.IsNullOrEmpty(request.UserId))
            {
                query = query.Where(x => x.UserId == request.UserId);
            }

            query = query
                .OrderQuery(request)
                .LimitQuery(request);
            
            var entities = await query.ToListAsync();

            return _mapper.Map<List<ProjectDTO>>(entities);
        }
    }
}
