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

namespace Blognet.Cms.Core.PageForwards.Queries.GetPageForwardsList
{
    public class GetPageForwardsListQueryHandler : IRequestHandler<GetPageForwardsListQuery, List<PageForwardDTO>>
    {
        private readonly IWebContext _context;
        private readonly IMapper _mapper;

        public GetPageForwardsListQueryHandler(IWebContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<PageForwardDTO>> Handle(GetPageForwardsListQuery request, CancellationToken cancellationToken)
        {
            var query = _context.PageForwards
                .AsQueryable()
                .Where(x => x.ProjectId == request.ProjectId)
                .OrderQuery(request)
                .LimitQuery(request);
            
            var entities = await query.ToListAsync(cancellationToken);

            return _mapper.Map<List<PageForwardDTO>>(entities);
        }
    }
}
