using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Blognet.Cms.Core.Interfaces;
using Blognet.Cms.Domain.Entities;
using Blognet.Cms.Domain.Enum;
using Blognet.Cms.Domain.Extensions;
using Blognet.Cms.Core.Model;
using AutoMapper;
using Blognet.Cms.Core.Albums.Commands.CreateAlbum;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Blognet.Cms.Domain.Exceptions;

namespace Blognet.Cms.Core.Articles.Commands.UpdateArticle
{
    public class UpdateArticleCommandHandler : IRequestHandler<UpdateArticleCommand, Unit>
    {
        private IWebContext _context;
        private IMapper _mapper;
        private IArticleAlbumService _articleAlbumService;
        private IArticleProcessorService _articleProcessorService;

        public UpdateArticleCommandHandler(
            IWebContext context,
            IMapper mapper,
            IArticleAlbumService articleAlbumService,
            IArticleProcessorService articleProcessorService
            )
        {
            _context = context;
            _mapper = mapper;
            _articleAlbumService = articleAlbumService;
            _articleProcessorService = articleProcessorService;
        }

        public async Task<Unit> Handle(UpdateArticleCommand request, CancellationToken cancellationToken)
        {
            using (var transaction = await _context.BeginTransactionAsync())
            {
                try
                {
                    var articleEntity = await _context.Articles
                        .Include(x => x.Project)
                        .Include(x => x.PhotoHeader)
                        .Include(x => x.PhotoThumbnail)
                        .Include(x => x.Seo)
                        .Include(x => x.ArticleSettings)
                        .Include(x => x.Category)
                        .FirstOrDefaultAsync(x => x.Id == request.Id);

                    if (articleEntity == null)
                    {
                        throw new EntityNotFoundException(nameof(Article), request.Id);
                    }

                    var seo = articleEntity.Seo;
                    if (seo == null && !request.Seo.IsEmpty)
                    {
                        articleEntity.Seo = new Seo();
                    }

                    if (seo != null)
                    {
                        articleEntity.Seo.Title = request.Seo.Title;
                        articleEntity.Seo.Description = request.Seo.Description;
                        articleEntity.Seo.Keywords = request.Seo.Keywords;
                    }

                    articleEntity.Title = request.Title;
                    articleEntity.Description = request.Description;
                    articleEntity.CategoryId = request.CategoryId;
                    articleEntity.Content = request.Content;
                    
                    //articleEntity.ArticleSettings =_mapper.Map<ArticleSettings>(request.ArticleSettings);
                    Mapper.Map<ArticleSettingsDTO, ArticleSettings>(request.ArticleSettings, articleEntity.ArticleSettings);

                    await _context.SaveChangesAsync(cancellationToken);

                    // Upload images
                    ArticleImagesDTO ArticleImagesDTO = _mapper.Map<ArticleImagesDTO>(request);
                    articleEntity = await _articleAlbumService.SaveArticleImages(articleEntity, ArticleImagesDTO);

                    // Process article content
                    List<Photo> images = await _context.Photos
                        .Where(x => x.AlbumId == articleEntity.AlbumId)
                        .Where(x => x.Type == PhotoType.Image)
                        .ToListAsync();

                    articleEntity.Content = _articleProcessorService.ProcessArticleImageContent(
                        articleEntity.Content,
                        articleEntity.Project.DomainName,
                        _mapper.Map<List<PhotoDTO>>(images));

                    await _context.SaveChangesAsync(cancellationToken);

                    transaction.Commit();

                    return Unit.Value;
                }
                catch(EntityNotFoundException e)
                {
                    throw e;
                }
                catch (Exception e)
                {
                    // @TODO handle uploaded images rollback
                    transaction.Rollback();

                    throw new EntityStoreException($"{nameof(Article)} store exception with message {e.Message}");
                }
            }
        }
    }
}
