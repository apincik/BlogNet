using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blognet.Cms.Core.Projects.Commands.CreateProject
{
    public class CreateProjectCommand : IRequest<int>
    {
        public string Name { get; set; }
        public string DomainName { get; set; }
        public string UserId { get; set; }
    }
}
