using AutoMapper;
using Core.Entities;
using System.Collections.Generic;
using WebApi.Model;

namespace WebApi.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Core.Entities.Article, ArticleListItem>()
                .ForMember(src => src.PhotoThumbnailImagePath, opt => opt.MapFrom(dest => dest.PhotoThumbnail.GetImagePath()));
            CreateMap<Core.Entities.Article, WebApi.Model.Article>()
                .ForMember(src => src.PhotoThumbnailImagePath, opt => opt.MapFrom(dest => dest.PhotoThumbnail.GetImagePath()))
                .ForMember(src => src.PhotoHeaderImagePath, opt => opt.MapFrom(dest => dest.PhotoHeader.GetImagePath()))
                .ForMember(src => src.CategoryTitle, opt => opt.MapFrom(dest => dest.Category.Title));
        }
    }
}
