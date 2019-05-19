using AutoMapper;
using Blognet.Cms.Core.Albums.Models;
using Blognet.Cms.Core.Model;
using Blognet.Cms.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blognet.Cms.Core.MappingProfiles
{
    public class AlbumMappingProfile : Profile
    {
        public AlbumMappingProfile()
        {
            CreateMap<Album, AlbumDetailViewModel>();
            CreateMap<Album, AlbumDTO>();
            CreateMap<Photo, PhotoDTO>()
                // Ignore to prevent additional selfreferencing from Album
                .ForMember(x => x.Album, y => y.Ignore());
            CreateMap<ArticleSettingsDTO, ArticleSettings>();
            CreateMap<ImageFileDTO, Photo>();
        }
    }
}
