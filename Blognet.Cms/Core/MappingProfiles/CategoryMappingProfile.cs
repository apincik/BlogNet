using AutoMapper;
using Blognet.Cms.Core.Albums.Models;
using Blognet.Cms.Core.Model;
using Blognet.Cms.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Blognet.Cms.Core.Categories.Models;

namespace Blognet.Cms.Core.MappingProfiles
{
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile()
        {
            CreateMap<Category, CategoryDTO>();
            CreateMap<Category, CategoryDetailViewModel>();
        }
    }
}
