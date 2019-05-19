using AutoMapper;
using Blognet.Cms.Core.Albums.Models;
using Blognet.Cms.Core.Albums.Queries.GetAlbumPhotosDetail;
using Blognet.Cms.Core.Interfaces;
using Blognet.Cms.Core.Model;
using Blognet.Cms.Domain.Entities;
using Blognet.Cms.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Blognet.Cms.Core.Albums.Queries.GetAlbumPhotosDetail
{
    public class GetAlbumPhotosDetailQueryHandler : IRequestHandler<GetAlbumPhotosDetailQuery, List<PhotoDTO>>
    {
        private readonly IWebContext _context;
        private readonly IMapper _mapper;

        public GetAlbumPhotosDetailQueryHandler(IWebContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<PhotoDTO>> Handle(GetAlbumPhotosDetailQuery request, CancellationToken cancellationToken)
        {
            var entities = await _context.Photos
                .Where(x => x.AlbumId == request.AlbumId)
                .ToListAsync();

            return _mapper.Map<List<PhotoDTO>>(entities);
        }
    }
}
