using AutoMapper;
using Blognet.Cms.Core.Extensions;
using Blognet.Cms.Core.Interfaces;
using Blognet.Cms.Core.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Blognet.Cms.Core.Categories.Queries.GetCategoriesList
{
    public class GetCategoriesListQueryHandler : IRequestHandler<GetCategoriesListQuery, List<CategoryDTO>>
    {
        private IWebContext _context;
        private IMapper _mapper;

        public GetCategoriesListQueryHandler(IWebContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<CategoryDTO>> Handle(GetCategoriesListQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Categories
                .AsQueryable()
                .Include(x => x.ParentCategory)
                .Where(x => x.ProjectId == request.ProjectId)
                .OrderQuery(request)
                .LimitQuery(request);

            var entities = await query.ToListAsync();

            return _mapper.Map<List<CategoryDTO>>(entities);
        }
    }
}
