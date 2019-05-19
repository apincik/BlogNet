using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Blognet.Cms.Core.Interfaces;
using Blognet.Cms.Domain.Entities;
using Blognet.Cms.Domain.Enum;
using Blognet.Cms.Domain.Extensions;
using Blognet.Cms.Core.Model;
using AutoMapper;
using Blognet.Cms.Core.Albums.Commands.CreateAlbum;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Blognet.Cms.Domain.Exceptions;

namespace Blognet.Cms.Core.Articles.Commands.DeleteArticle
{
    public class DeleteArticleCommandHandler : IRequestHandler<DeleteArticleCommand, Unit>
    {
        private IWebContext _context;
        public DeleteArticleCommandHandler(
            IWebContext context
            )
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteArticleCommand request, CancellationToken cancellationToken)
        {
            var articleEntity = await _context.Articles
                .Include(x => x.Project)
                .Include(x => x.PhotoHeader)
                .Include(x => x.PhotoThumbnail)
                .Include(x => x.Seo)
                .Include(x => x.ArticleSettings)
                .Include(x => x.Category)
                .Include(x => x.Album)
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            if(articleEntity == null)
            {
                throw new EntityNotFoundException(nameof(Article), request.Id);
            }

            try
            {
                _context.Articles.Remove(articleEntity);

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
            catch (Exception e)
            {

                throw new EntityDeleteException($"{nameof(Article)} delete exception with message {e.Message}");
            }
        }
    }
}
