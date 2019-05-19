using System;
using System.Collections.Generic;
using System.Text;

namespace Blognet.Cms.Core.Model
{
    public class ProjectDTO
    {
        public int? Id { get; set; }

        public string UserId { get; set; }

        public string Name { get; set; }

        public string DomainName { get; set; }

        public string Description { get; set; }

        public ProjectSettingsDTO ProjectSettings { get; set; }

        public DateTime CreatedAt { get; internal set; }

        public DateTime UpdatedAt { get; private set; }
    }
}
