using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Blognet.Cms.Domain.Entities;
using Blognet.Cms.Domain.Enum;
using Microsoft.AspNetCore.Http;
using Blognet.Cms.Core.Model;
namespace Blognet.Cms.Core.Articles.Commands.DeleteArticle
{
    public class DeleteArticleCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
