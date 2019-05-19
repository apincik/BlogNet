using AutoMapper;
using Blognet.Cms.Core.Articles.Models;
using Blognet.Cms.Core.Interfaces;
using Blognet.Cms.Domain.Entities;
using Blognet.Cms.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Blognet.Cms.Core.Articles.Queries.GetArticleDetail
{
    public class GetArticleDetailQueryHandler : IRequestHandler<GetArticleDetailQuery, ArticleDetailViewModel>
    {
        private readonly IWebContext _context;
        private readonly IMapper _mapper;

        public GetArticleDetailQueryHandler(IWebContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ArticleDetailViewModel> Handle(GetArticleDetailQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Articles
                .Include(x => x.Project)
                .Include(x => x.PhotoHeader)
                .Include(x => x.PhotoThumbnail)
                .Include(x => x.Seo)
                .Include(x => x.ArticleSettings)
                .Include(x => x.Category)
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            if (entity == null)
            {
                throw new EntityNotFoundException(nameof(Article), request.Id);
            }

            return _mapper.Map<ArticleDetailViewModel>(entity);
        }
    }
}
