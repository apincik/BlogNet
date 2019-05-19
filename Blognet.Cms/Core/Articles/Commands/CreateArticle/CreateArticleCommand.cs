using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Blognet.Cms.Domain.Entities;
using Blognet.Cms.Domain.Enum;
using Microsoft.AspNetCore.Http;
using Blognet.Cms.Core.Model;

namespace Blognet.Cms.Core.Articles.Commands.CreateArticle
{
    public class CreateArticleCommand : IRequest<int>
    {
        public int ProjectId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Content { get; set; }

        public string Slug { get; set; }

        public string Tags { get; set; }

        public int CategoryId { get; set; }

        public SeoDTO Seo { get; set; }

        IFormFile FileThumbnail { get; set; }

        public IFormFile FileHeader { get; set; }

        public List<IFormFile> Files { get; set; }

        public string RemoteFileThumbnail { get; set; }

        public string RemoteFileHeader { get; set; }

        public string RemoteFiles { get; set; }
    }
}
