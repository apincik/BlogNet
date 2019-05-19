using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Blognet.Cms.Core.Interfaces;
using Blognet.Cms.Domain.Entities;
using Blognet.Cms.Domain.Enum;

namespace Blognet.Cms.Core.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, int>
    {
        private IWebContext _context;

        public CreateCategoryCommandHandler(IWebContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            Seo seoEntity = null;
            if(!request.Seo.IsEmpty)
            {
                seoEntity = new Seo
                {
                    Title = request.Seo.Title,
                    Description = request.Seo.Description,
                    Keywords = request.Seo.Keywords
                };

                _context.Seo.Add(seoEntity);
            }

            var entity = new Category
            {
                ProjectId = request.ProjectId,
                Title = request.Title,
                Description = request.Description,
                ParentCategoryId = request.ParentCategoryId,
                Status = Status.Active,
                Seo = seoEntity
            };

            _context.Categories.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
