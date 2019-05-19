using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Blognet.Cms.Core.Interfaces;
using Blognet.Cms.Domain.Entities;
using Blognet.Cms.Domain.Exceptions;

namespace Blognet.Cms.Core.Categories.Commands.UpdateCategory { 
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Unit>
    {
        private IWebContext _context;

        public UpdateCategoryCommandHandler(IWebContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Categories.FindAsync(request.Id);
            if(entity == null)
            {
                throw new EntityNotFoundException(nameof(Article), request.Id);
            }

            // @TODO move SEO to own service, create interface for DTOs
            // Do not remove SEO record on existing and null values
            Seo seoEntity = null;
            if (!request.Seo.IsEmpty && entity.Seo == null)
            {
                seoEntity = new Seo
                {
                    Title = request.Seo.Title,
                    Description = request.Seo.Description,
                    Keywords = request.Seo.Keywords
                };

                _context.Seo.Add(seoEntity);
            } 
            else if(entity.Seo != null)
            {
                entity.Seo.Title = request.Seo.Title;
                entity.Seo.Description = request.Seo.Description;
                entity.Seo.Keywords = request.Seo.Keywords;
            }

            entity.Title = request.Title;
            entity.Description = request.Description;
            entity.ParentCategoryId = request.ParentCategoryId;
            entity.Seo = seoEntity;
            
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
