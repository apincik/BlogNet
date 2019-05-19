using MediatR;
using Blognet.Cms.Core.Projects.Models;

namespace Blognet.Cms.Core.Projects.Queries.GetProjectDetail
{
    public class GetProjectDetailQuery : IRequest<ProjectDetailViewModel>
    {
        public int Id;

        
    }
}
