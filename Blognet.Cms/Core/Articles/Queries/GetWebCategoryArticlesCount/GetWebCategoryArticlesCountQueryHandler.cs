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

namespace Blognet.Cms.Core.Articles.Queries.GetWebCategoryArticlesCount
{
    public class GetWebCategoryArticlesCountQueryHandler : IRequestHandler<GetWebCategoryArticlesCountQuery, int>
    {
        private IWebContext _context;

        public GetWebCategoryArticlesCountQueryHandler(IWebContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(GetWebCategoryArticlesCountQuery request, CancellationToken cancellationToken)
        {
            var category = await _context.Categories
                .Include(c => c.SubCategories)
                .Where(c => c.Title.ToLower() == request.CategoryTitle.ToLower())
                .FirstOrDefaultAsync();

            List<int> categories = category != null ? category.SubCategoryTreeIds : new List<int>();

            return await _context.Articles
                .Where(a => a.Project.DomainName == request.ProjectDomain)
                .Where(a => categories.Contains(a.Category.Id))
                .CountAsync();
        }
    }
}
