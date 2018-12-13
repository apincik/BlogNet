using AutoMapper;
using Core.Entities;
using Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Areas.Account.ViewModels;
using Web.Areas.Account.ViewModels.Projects;
using Web.Areas.Cms.ViewModels;
using Web.Areas.Cms.ViewModels.Albums;
using Web.Areas.Cms.ViewModels.Articles;
using Web.Areas.Cms.ViewModels.Categories;
using Web.Areas.Web.ViewModels.MenuItems;
using Web.Areas.Web.ViewModels.PageForwards;
using Web.Areas.Web.ViewModels.Variables;

namespace Web.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Project, ProjectCreateViewModel>();
            CreateMap<ProjectCreateViewModel, Project>();
            CreateMap<Project, ProjectUpdateViewModel>();
            CreateMap<ProjectUpdateViewModel, Project>();

            CreateMap<Category, CategoryCreateViewModel>();
            CreateMap<CategoryCreateViewModel, Category>();
            CreateMap<Category, CategoryUpdateViewModel>();
            CreateMap<CategoryUpdateViewModel, Category>();

            CreateMap<Album, AlbumCreateViewModel>();
            CreateMap<AlbumCreateViewModel, Album>();
            CreateMap<Album, AlbumUpdateViewModel>();
            CreateMap<AlbumUpdateViewModel, Album>();

            CreateMap<Article, ArticleCreateViewModel>();
            CreateMap<ArticleCreateViewModel, Article>();
            CreateMap<Article, ArticleUpdateViewModel>();
            CreateMap<ArticleUpdateViewModel, Article>();

            CreateMap<TemplateVariable, VariableCreateViewModel>();
            CreateMap<VariableCreateViewModel, TemplateVariable>();
            CreateMap<TemplateVariable, VariableUpdateViewModel>();
            CreateMap<VariableUpdateViewModel, TemplateVariable>();

            CreateMap<PageForward, PageForwardCreateViewModel>();
            CreateMap<PageForwardCreateViewModel, PageForward>();
            CreateMap<PageForward, PageForwardUpdateViewModel>();
            CreateMap<PageForwardUpdateViewModel, PageForward>();

            CreateMap<MenuItem, MenuItemCreateViewModel>();
            CreateMap<MenuItemCreateViewModel, MenuItem>();
            CreateMap<MenuItem, MenuItemUpdateViewModel>();
            CreateMap<MenuItemUpdateViewModel, MenuItem>();

            CreateMap<ImageFileDto, Photo>();

            CreateMap<ArticleCreateViewModel, ArticleImagesDto>();
                //.ForMember(userEdit => userEdit.RemoteFiles, opt => opt.MapFrom(user => user.RemoteFiles.Split(';').ToList()));

            CreateMap<ArticleUpdateViewModel, ArticleImagesDto>();
        }
    }
}
