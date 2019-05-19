using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Blognet.Cms.Core.Interfaces;
using Blognet.Cms.Domain.Entities;
using Blognet.Cms.Domain.Exceptions;
using Blognet.Cms.Domain.Extensions;
using Blognet.Cms.Domain.Enum;

namespace Blognet.Cms.Core.Articles.Commands.ToggleArticleStatus
{ 
    public class ToggleArticleStatusCommandHandler : IRequestHandler<ToggleArticleStatusCommand, Unit>
    {
        private IWebContext _context;

        public ToggleArticleStatusCommandHandler(IWebContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(ToggleArticleStatusCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Articles.FindAsync(request.Id);
            if(entity == null)
            {
                throw new EntityNotFoundException(nameof(Album), request.Id);
            }

            entity.Status = entity.Status == ArticleStatus.Inactive ? ArticleStatus.Unpublished :
                (entity.Status == ArticleStatus.Unpublished ? ArticleStatus.Published :
                (entity.Status == ArticleStatus.Published ? ArticleStatus.Unpublished : ArticleStatus.Inactive));

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
