using AutoMapper;
using Blognet.Cms.Core.Articles.Commands.CreateArticle;
using Blognet.Cms.Core.MenuItems.Models;
using Blognet.Cms.Core.Model;
using Blognet.Cms.Core.Projects.Models;
using Blognet.Cms.Domain.Entities;

namespace Blognet.Cms.Core.MappingProfiles
{
    public class MenuItemsMappingProfile : Profile
    {
        public MenuItemsMappingProfile()
        {
            CreateMap<MenuItem, MenuItemDTO>();
            CreateMap<MenuItem, MenuItemDetailViewModel>();
        }
    }
}
