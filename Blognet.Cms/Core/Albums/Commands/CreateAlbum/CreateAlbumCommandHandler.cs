using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Blognet.Cms.Core.Interfaces;
using Blognet.Cms.Domain.Entities;
using Blognet.Cms.Domain.Extensions;
using Blognet.Cms.Domain.Enum;

namespace Blognet.Cms.Core.Albums.Commands.CreateAlbum
{
    public class CreateAlbumCommandHandler : IRequestHandler<CreateAlbumCommand, int>
    {
        private IWebContext _context;

        public CreateAlbumCommandHandler(IWebContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateAlbumCommand request, CancellationToken cancellationToken)
        {
            var entity = new Album
            {
                Name = request.Name,
                NameNormalized = request.Name.GenerateSlug(),
                Status = request.Status == null ? Status.Inactive : request.Status.Value,
                Type = request.AlbumType == null ? AlbumType.Custom : request.AlbumType.Value,
            };

            _context.Albums.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
