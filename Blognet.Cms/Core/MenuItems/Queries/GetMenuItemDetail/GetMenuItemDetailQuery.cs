using Blognet.Cms.Core.Categories.Models;
using Blognet.Cms.Core.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Blognet.Cms.Core.MenuItems.Models;

namespace Blognet.Cms.Core.MenuItems.Queries.GetMenuItemDetail
{
    public class GetMenuItemDetailQuery : IRequest<MenuItemDetailViewModel>
    {
        public int Id;

        public int ProjectId { get; set; }

        public string ParentCategoryId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public SeoDTO Seo { get; set; }
    }
}
