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
    public class GetArticleBySlugHandler : IRequestHandler<GetArticleBySlugCommand, ArticleResult>
    {
        private IMapper _mapper;
        private IArticleRepository _articleRepository;

        public GetArticleBySlugHandler(IMapper mapper, IArticleRepository articleRepository)
        {
            _mapper = mapper;
            _articleRepository = articleRepository;
        }

        public async Task<ArticleResult> Handle(GetArticleBySlugCommand request, CancellationToken cancellationToken)
        {
            Core.Entities.Article article = await _articleRepository.GetProjectArticleBySlug(request.ProjectDomain, request.Slug);

            return new ArticleResult
            {
                Article = _mapper.Map<Article>(article)
            };
        }
    }
}
