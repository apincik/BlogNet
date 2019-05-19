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

namespace Blognet.Cms.Core.TemplateVariables.Queries.GetTemplateVariablesList
{
    public class GetTemplateVariablesListQueryHandler : IRequestHandler<GetTemplateVariablesListQuery, List<TemplateVariableDTO>>
    {
        private readonly IWebContext _context;
        private readonly IMapper _mapper;

        public GetTemplateVariablesListQueryHandler(IWebContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<TemplateVariableDTO>> Handle(GetTemplateVariablesListQuery request, CancellationToken cancellationToken)
        {
            var query = _context.TemplateVariables
                .AsQueryable()
                .Include(x => x.ParentTemplateVariable)
                .Include(x => x.TemplateVariables)
                .Where(x => x.ProjectId == request.ProjectId)
                .OrderQuery(request)
                .LimitQuery(request);
            
            var entities = await query.ToListAsync();

            return _mapper.Map<List<TemplateVariableDTO>>(entities);
        }
    }
}
