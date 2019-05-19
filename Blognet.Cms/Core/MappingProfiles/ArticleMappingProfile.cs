using AutoMapper;
using Blognet.Cms.Core.Albums.Models;
using Blognet.Cms.Core.Articles.Commands.UpdateArticle;
using Blognet.Cms.Core.Articles.Models;
using Blognet.Cms.Core.Model;
using Blognet.Cms.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blognet.Cms.Core.MappingProfiles
{
    public class ArticleMappingProfile : Profile
    {
        public ArticleMappingProfile()
        {
            CreateMap<ArticleSettings, ArticleSettingsDTO>();

            CreateMap<ArticleSettingsDTO, ArticleSettings>()
                .ForMember(x => x.Id, y => y.Ignore())
                .ForMember(x => x.ArticleId, y => y.Ignore());

            CreateMap<Article, ArticleDetailViewModel>()
                .ForMember(x => x.FileThumbnail, y => y.Ignore())
                .ForMember(x => x.FileHeader, y => y.Ignore())
                .ForMember(x => x.Files, y => y.Ignore())
                .ForMember(x => x.RemoteFileThumbnail, y => y.Ignore())
                .ForMember(x => x.RemoteFileHeader, y => y.Ignore())
                .ForMember(x => x.RemoteFiles, y => y.Ignore())
                .ForMember(x => x.Categories, y => y.Ignore());

            CreateMap<ArticleListItem, Article>();

            CreateMap<UpdateArticleCommand, ArticleImagesDTO>();
        }
    }
}
