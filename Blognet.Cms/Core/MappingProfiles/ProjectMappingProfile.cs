using AutoMapper;
using Blognet.Cms.Core.Articles.Commands.CreateArticle;
using Blognet.Cms.Core.Model;
using Blognet.Cms.Core.Projects.Models;
using Blognet.Cms.Domain.Entities;

namespace Blognet.Cms.Core.MappingProfiles
{
    public class ProjectMappingProfile : Profile
    {
        public ProjectMappingProfile()
        {
            CreateMap<Project, ProjectDetailViewModel>();
            CreateMap<Project, ProjectDTO>();
            CreateMap<ProjectSettingsDTO, ProjectSettings>();
        }
    }
}
