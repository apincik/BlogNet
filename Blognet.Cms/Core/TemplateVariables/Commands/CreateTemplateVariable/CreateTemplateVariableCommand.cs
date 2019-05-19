using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Blognet.Cms.Core.Model;
using Blognet.Cms.Domain.Entities;
using Blognet.Cms.Domain.Enum;

namespace Blognet.Cms.Core.TemplateVariables.Commands.CreateTemplateVariable
{
    public class CreateTemplateVariableCommand : IRequest<int>
    {

        public int ProjectId { get; set; }

        public string Label { get; set; }
        public string Content { get; set; }

        public ProjectVariableType Type { get; set; }

        public bool ShowRaw { get; set; }

        public int? ParentTemplateVariableId { get; set; }

        public List<TemplateVariableDTO> Variables { get; set; }
    }
}
