using AutoMapper;
using Blognet.Cms.Core.Articles.Models;
using Blognet.Cms.Core.Interfaces;
using Blognet.Cms.Core.Model;
using Blognet.Cms.Domain.Enum;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Blognet.Cms.Core.Articles.Queries.GetWebForwardedArticle
{
    public class GetWebForwardedArticleQueryHandler : IRequestHandler<GetWebForwardedArticleQuery, WebArticleModel>
    {
        private IWebContext _context;
        private IMapper _mapper;

        public GetWebForwardedArticleQueryHandler(IWebContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // @TODO throw exception
        public async Task<WebArticleModel> Handle(GetWebForwardedArticleQuery request, CancellationToken cancellationToken)
        {
            // @TODO move to controller
            //string maskParam = System.Uri.UnescapeDataString(request.Mask);

            var forward = await _context.PageForwards
                .Include(m => m.Project)
                .Where(m => m.Project.DomainName == request.ProjectDomain)
                .Where(m => m.Type == PageForwardSourceType.Article)
                .Where(m => m.Mask == request.Mask)
                .FirstOrDefaultAsync();

            if (forward == null)
            {
                return null;
            }

            var article = await _context.Articles.FindAsync(int.Parse(forward.SourceId));

            return new WebArticleModel
            {
                Article = _mapper.Map<ArticleDTO>(article)
            };
        }
    }
}