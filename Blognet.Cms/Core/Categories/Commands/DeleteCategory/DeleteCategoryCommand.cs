using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blognet.Cms.Core.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
