using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.CommandResults;

namespace WebApi.Commands
{
    public class GetArticlesByCategoryCommand : IRequest<CategoryArticlesResult>
    {
        public string ProjectDomain { get; set; }
        public string CategoryTitle { get; set; }
        public int Page { get; set; } = 0;
        public int Limit { get; set; } = 10;
    }
}
