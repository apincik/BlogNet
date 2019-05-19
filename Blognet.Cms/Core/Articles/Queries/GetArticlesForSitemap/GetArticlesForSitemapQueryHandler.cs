using AutoMapper;
using Blognet.Cms.Core.Articles.Models;
using Blognet.Cms.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Blognet.Cms.Core.Articles.Queries.GetArticlesForSitemap
{
    public class GetArticlesForSitemapQueryHandler : IRequestHandler<GetArticlesForSitemapQuery, SitemapArticleModel>
    {
        private IWebContext _context;
        private IMapper _mapper;

        public GetArticlesForSitemapQueryHandler(IWebContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SitemapArticleModel> Handle(GetArticlesForSitemapQuery request, CancellationToken cancellationToken)
        {
            return await Task.FromResult<SitemapArticleModel>(null);
        }
    }
}
