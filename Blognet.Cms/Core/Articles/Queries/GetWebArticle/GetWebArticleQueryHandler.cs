using AutoMapper;
using Blognet.Cms.Core.Articles.Models;
using Blognet.Cms.Core.Interfaces;
using Blognet.Cms.Core.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace Blognet.Cms.Core.Articles.Queries.GetWebArticle
{
    public class GetWebArticleQueryHandler : IRequestHandler<GetWebArticleQuery, WebArticleModel>
    {
        private IWebContext _context;
        private IMapper _mapper;

        public GetWebArticleQueryHandler(IWebContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<WebArticleModel> Handle(GetWebArticleQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Articles
                .AsNoTracking()
                .Include(a => a.Category)
                .Include(a => a.Seo)
                .Include(a => a.PhotoHeader)
                .Include(a => a.PhotoThumbnail)
                .Include(a => a.Album)
                    .ThenInclude(album => album.Photos)
                .Include(a => a.ArticleSettings)
                .Include(a => a.Project)
                .Where(a => a.Project.DomainName == request.ProjectDomain)
                .Where(a => a.Slug == request.Slug)
                .FirstOrDefaultAsync();

            return new WebArticleModel
            {
                Article = _mapper.Map<ArticleDTO>(entity)
            };
        }
    }
}
