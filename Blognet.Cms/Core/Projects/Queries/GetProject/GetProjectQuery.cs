using MediatR;
using Blognet.Cms.Core.Projects.Models;
using Blognet.Cms.Core.Model;

namespace Blognet.Cms.Core.Projects.Queries.GetProjectDetail
{
    public class GetProjectQuery : IRequest<ProjectDTO>
    {
        public int Id;

        
    }
}
