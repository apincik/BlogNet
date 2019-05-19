using AutoMapper;
using Blognet.Cms.Core.Interfaces;
using Blognet.Cms.Core.MenuItems.Models;
using Blognet.Cms.Core.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Blognet.Cms.Core.MenuItems.Queries.GetWebMenu
{
    public class GetWebMenuQueryHandler : IRequestHandler<GetWebMenuQuery, WebMenuModel>
    {
        private readonly IWebContext _context;
        private readonly IMapper _mapper;

        public GetWebMenuQueryHandler(IWebContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<WebMenuModel> Handle(GetWebMenuQuery request, CancellationToken cancellationToken)
        {
            List<Blognet.Cms.Domain.Entities.MenuItem> menuItems = await _context.MenuItems
                .Include(m => m.MenuItems)
                .Where(m => m.ParentMenuId == null)
                .Where(m => m.Project.DomainName == request.ProjectDomain)
                .Select(m => new Blognet.Cms.Domain.Entities.MenuItem { Title = m.Title, Url = m.Url, MenuItems = m.MenuItems })
                .ToListAsync();

            return new WebMenuModel
            {
                MenuItems = _mapper.Map<List<MenuItemDTO>>(menuItems)
            };
        }
    }
}
