using AutoMapper;
using Core.Interfaces;
using Core.Interfaces.Repositories;
using Core.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApi.CommandResults;
using WebApi.Commands;
using WebApi.Model;

namespace WebApi.CommandsHandlers
{
    public class GetArticlesByCategoryHandler : IRequestHandler<GetArticlesByCategoryCommand, CategoryArticlesResult>
    {
        private IMapper _mapper;
        private IArticleRepository _articleRepository;

        public GetArticlesByCategoryHandler(IMapper mapper, IArticleRepository articleRepository)
        {
            _mapper = mapper;
            _articleRepository = articleRepository;
        }

        public async Task<CategoryArticlesResult> Handle(GetArticlesByCategoryCommand request, CancellationToken cancellationToken)
        {
            var articles = await _articleRepository.ListProjectArticlesByCategoryTitle(request.ProjectDomain, request.CategoryTitle, request.Limit, request.Page);
            int categoryArticlesCount = await _articleRepository.CountProjectArticlesByCategoryTitle(request.ProjectDomain, request.CategoryTitle);

            return new CategoryArticlesResult
            {
                Articles = _mapper.Map<List<ArticleListItem>>(articles),
                ArticlesCount = categoryArticlesCount,
                Page = request.Page,
                Limit = request.Limit
            };
        }
    }
}
