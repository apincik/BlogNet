using AutoMapper;
using Blognet.Cms.Core.Albums.Models;
using Blognet.Cms.Core.Interfaces;
using Blognet.Cms.Domain.Entities;
using Blognet.Cms.Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Blognet.Cms.Core.Albums.Queries.GetAlbumDetail
{
    public class GetAlbumDetailQueryHandler : IRequestHandler<GetAlbumDetailQuery, AlbumDetailViewModel>
    {
        private readonly IWebContext _context;
        private readonly IMapper _mapper;

        public GetAlbumDetailQueryHandler(IWebContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AlbumDetailViewModel> Handle(GetAlbumDetailQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Albums
                .FindAsync(request.Id);
            
            if(entity == null)
            {
                throw new EntityNotFoundException(nameof(Album), request.Id);
            }

            return _mapper.Map<AlbumDetailViewModel>(entity);
        }
    }
}
