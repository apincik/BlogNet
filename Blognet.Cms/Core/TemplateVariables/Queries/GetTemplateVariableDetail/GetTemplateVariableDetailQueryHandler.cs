using AutoMapper;
using Blognet.Cms.Core.Interfaces;
using Blognet.Cms.Core.TemplateVariables.Models;
using Blognet.Cms.Domain.Entities;
using Blognet.Cms.Domain.Exceptions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Blognet.Cms.Core.TemplateVariables.Queries.GetTemplateVariableDetail
{
    public class GetTemplateVariableDetailQueryHandler : IRequestHandler<GetTemplateVariableDetailQuery, TemplateVariableDetailViewModel>
    {
        private readonly IWebContext _context;
        private readonly IMapper _mapper;

        public GetTemplateVariableDetailQueryHandler(IWebContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TemplateVariableDetailViewModel> Handle(GetTemplateVariableDetailQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.TemplateVariables
                .FindAsync(request.Id);
            
            if(entity == null)
            {
                throw new EntityNotFoundException(nameof(TemplateVariable), request.Id.ToString());
            }

            return _mapper.Map<TemplateVariableDetailViewModel>(entity);
        }
    }
}
