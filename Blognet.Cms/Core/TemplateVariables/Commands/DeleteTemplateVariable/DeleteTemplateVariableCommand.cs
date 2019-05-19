using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blognet.Cms.Core.TemplateVariables.Commands.DeleteTemplateVariable
{
    public class DeleteTemplateVariableCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
