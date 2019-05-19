using AutoMapper;
using Blognet.Cms.Core.Articles.Commands.CreateArticle;
using Blognet.Cms.Core.Model;
using Blognet.Cms.Core.Projects.Models;
using Blognet.Cms.Core.TemplateVariables.Models;
using Blognet.Cms.Domain.Entities;

namespace Blognet.Cms.Core.MappingProfiles
{
    public class DefaultMappings : Profile
    {
        public DefaultMappings()
        {
            CreateMap<CreateArticleCommand, ArticleImagesDTO>();
            CreateMap<PageForward, PageForwardDTO>();
            CreateMap<TemplateVariable, TemplateVariableDTO>();
            CreateMap<TemplateVariable, TemplateVariableDetailViewModel>();
        }
    }
}
