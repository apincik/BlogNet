using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blognet.Cms.Core.MenuItems.Commands.CreateMenuItem
{
    public class CreateMenuItemCommand : IRequest<int>
    {
        public int ProjectId { get; set; }

        public string Title { get; set; }

        public string Url { get; set; }

        public int? ParentMenuId { get; set; }

    }
}
