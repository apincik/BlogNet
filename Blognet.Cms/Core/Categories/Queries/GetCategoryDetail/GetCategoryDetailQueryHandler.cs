using AutoMapper;
using Blognet.Cms.Core.Categories.Models;
using Blognet.Cms.Core.Interfaces;
using Blognet.Cms.Domain.Entities;
using Blognet.Cms.Domain.Exceptions;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Blognet.Cms.Core.Categories.Queries.GetCategoryDetail
{
    public class GetCategoryDetailQueryHandler : IRequestHandler<GetCategoryDetailQuery, CategoryDetailViewModel>
    {
        private readonly IWebContext _context;
        private readonly IMapper _mapper;

        public GetCategoryDetailQueryHandler(IWebContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CategoryDetailViewModel> Handle(GetCategoryDetailQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Categories
                .Include(x => x.Seo)
                .Where(x => x.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);


            if (entity == null)
            {
                throw new EntityNotFoundException(nameof(Category), request.Id.ToString());
            }

            return _mapper.Map<CategoryDetailViewModel>(entity);
        }
    }
}
