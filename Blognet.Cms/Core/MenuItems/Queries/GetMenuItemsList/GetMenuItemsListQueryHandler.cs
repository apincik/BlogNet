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

namespace Blognet.Cms.Core.MenuItems.Queries.GetMenuItemsList
{
    public class GetMenuItemsListQueryHandler : IRequestHandler<GetMenuItemsListQuery, List<MenuItemDTO>>
    {
        private readonly IWebContext _context;
        private readonly IMapper _mapper;

        public GetMenuItemsListQueryHandler(IWebContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<MenuItemDTO>> Handle(GetMenuItemsListQuery request, CancellationToken cancellationToken)
        {
            var query = _context.MenuItems
                .AsQueryable()
                .Include(x => x.MenuItems)
                .Include(x => x.ParentMenu)
                .Where(x => x.ProjectId == request.ProjectId)
                .OrderQuery(request)
                .LimitQuery(request);
            
            var entities = await query.ToListAsync();

            return _mapper.Map<List<MenuItemDTO>>(entities);
        }
    }
}
