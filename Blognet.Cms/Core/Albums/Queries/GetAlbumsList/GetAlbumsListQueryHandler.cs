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

namespace Blognet.Cms.Core.Albums.Queries.GetAlbumsList
{
    public class GetAlbumsListQueryHandler : IRequestHandler<GetAlbumsListQuery, List<AlbumDTO>>
    {
        private readonly IWebContext _context;
        private readonly IMapper _mapper;

        public GetAlbumsListQueryHandler(IWebContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<AlbumDTO>> Handle(GetAlbumsListQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Albums
                .AsQueryable()
                .OrderQuery(request)
                .LimitQuery(request);
            
            var entities = await query.ToListAsync();

            return _mapper.Map<List<AlbumDTO>>(entities);
        }
    }
}
