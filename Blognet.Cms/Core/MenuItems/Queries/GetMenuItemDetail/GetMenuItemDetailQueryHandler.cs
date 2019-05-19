using AutoMapper;
using Blognet.Cms.Core.Interfaces;
using Blognet.Cms.Core.MenuItems.Models;
using Blognet.Cms.Domain.Entities;
using Blognet.Cms.Domain.Exceptions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using MenuItem = Blognet.Cms.Domain.Entities.MenuItem;

namespace Blognet.Cms.Core.MenuItems.Queries.GetMenuItemDetail
{
    public class GetMenuItemDetailQueryHandler : IRequestHandler<GetMenuItemDetailQuery, MenuItemDetailViewModel>
    {
        private readonly IWebContext _context;
        private readonly IMapper _mapper;

        public GetMenuItemDetailQueryHandler(IWebContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<MenuItemDetailViewModel> Handle(GetMenuItemDetailQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.MenuItems
                .FindAsync(request.Id);
            
            if(entity == null)
            {
                throw new EntityNotFoundException(nameof(MenuItem), request.Id.ToString());
            }

            return _mapper.Map<MenuItemDetailViewModel>(entity);
        }
    }
}
