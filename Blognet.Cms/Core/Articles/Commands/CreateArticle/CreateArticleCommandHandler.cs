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

namespace Blognet.Cms.Core.Articles.Commands.CreateArticle
{
    public class CreateArticleCommandHandler : IRequestHandler<CreateArticleCommand, int>
    {
        private IWebContext _context;
        private IMapper _mapper;
        private IArticleAlbumService _articleAlbumService;
        private IArticleProcessorService _articleProcessorService;

        public CreateArticleCommandHandler(
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

        public async Task<int> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
        {
            using (var transaction = await _context.BeginTransactionAsync())
            {
                try
                {
                    var articleEntity = new Article
                    {
                        Project = await _context.Projects.FindAsync(request.ProjectId),
                        Title = request.Title,
                        Description = request.Description,
                        CategoryId = request.CategoryId,
                        Content = request.Content,
                        Status = ArticleStatus.Inactive,
                        Slug = request.Title.GenerateSlug(),
                        Seo = request.Seo.IsEmpty ? null : new Seo
                        {
                            Title = request.Seo.Title,
                            Description = request.Seo.Description,
                            Keywords = request.Seo.Keywords
                        }
                    };

                    _context.Articles.Add(articleEntity);
                    await _context.SaveChangesAsync(cancellationToken);

                    // Create article settings
                    var articleSettings = _mapper.Map<ArticleSettings>(new ArticleSettingsDTO());
                    articleSettings.ArticleId = articleEntity.Id;
                    _context.ArticleSettings.Add(articleSettings);
                    await _context.SaveChangesAsync(cancellationToken);

                    // Create album for article
                    articleEntity.AlbumId = await _articleAlbumService.CreateAlbum(articleEntity);
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

                    return articleEntity.Id;
                }
                catch(Exception e)
                {
                    // @TODO handle uploaded images rollback
                    transaction.Rollback();

                    throw new EntityStoreException($"{nameof(Article)} store exception with message {e.Message}");
                }
            }
        }
    }
}
