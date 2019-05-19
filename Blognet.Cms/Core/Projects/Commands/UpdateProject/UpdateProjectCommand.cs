using Blognet.Cms.Core.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blognet.Cms.Core.Projects.Commands.UpdateProject
{
    public class UpdateProjectCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DomainName { get; set; }
        public string Description { get; set; }
        public ProjectSettingsDTO ProjectSettings {get;set;}
    }
}
