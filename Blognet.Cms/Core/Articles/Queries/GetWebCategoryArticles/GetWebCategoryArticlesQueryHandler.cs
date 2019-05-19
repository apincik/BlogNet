using AutoMapper;
using Blognet.Cms.Core.Articles.Models;
using Blognet.Cms.Core.Interfaces;
using Blognet.Cms.Core.Model;
using Blognet.Cms.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Blognet.Cms.Core.Articles.Queries.GetWebCategoryArticles
{
    public class GetWebCategoryArticlesQueryHandler : IRequestHandler<GetWebCategoryArticlesQuery, WebCategoryArticlesModel>
    {
        private IWebContext _context;
        private IMapper _mapper;

        public GetWebCategoryArticlesQueryHandler(IWebContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // @TODO Customize query for ordering, selection and other parameters if needed.
        public async Task<WebCategoryArticlesModel> Handle(GetWebCategoryArticlesQuery request, CancellationToken cancellationToken)
        {
            var category = await _context.Categories
                .Include(c => c.SubCategories)
                .Where(c => c.Title.ToLower() == request.CategoryTitle.ToLower())
                .FirstOrDefaultAsync();

            List<int> categories = category != null ? category.SubCategoryTreeIds : new List<int>();

            var articles = await _context.Articles
                .Include(a => a.PhotoThumbnail)
                .Where(a => a.Project.DomainName == request.ProjectDomain)
                .Where(a => categories.Contains(a.Category.Id))
                .OrderByDescending(c => c.Id)
                .Skip(request.Limit * request.Page)
                .Take(request.Limit)
                .Select(a => new Article
                {
                    Id = a.Id,
                    Title = a.Title,
                    Description = a.Description,
                    PhotoThumbnail = a.PhotoThumbnail,
                    Slug = a.Slug
                })
                .AsNoTracking()
                .ToListAsync();

            return new WebCategoryArticlesModel
            {
                Articles = _mapper.Map<List<ArticleListItem>>(articles),
                Limit = request.Limit,
                Page = request.Page
            };
        }
    }
}
