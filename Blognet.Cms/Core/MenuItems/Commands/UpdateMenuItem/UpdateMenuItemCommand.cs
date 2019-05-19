using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blognet.Cms.Core.MenuItems.Commands.UpdateMenuItem
{
    public class UpdateMenuItemCommand : IRequest<Unit>
    {
        public int Id { get; set; }

        public int ProjectId { get; set; }

        public string Title { get; set; }

        public string Url { get; set; }

        public int? ParentMenuId { get; set; }
    }
}
