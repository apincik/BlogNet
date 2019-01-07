using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.CommandResults;

namespace WebApi.Commands
{
    public class GetArticleBySlugCommand : IRequest<ArticleResult>
    {
        public string ProjectDomain { get; set; }
        public string Slug { get; set; }
    }
}
