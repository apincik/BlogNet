using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blognet.Cms.Core.MenuItems.Commands.DeleteMenuItem
{
    public class DeleteMenuItemCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
