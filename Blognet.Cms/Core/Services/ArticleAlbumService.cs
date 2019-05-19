using Blognet.Cms.Core.Albums.Commands.CreateAlbum;
using Blognet.Cms.Core.Albums.Commands.UploadPhotos;
using Blognet.Cms.Core.Interfaces;
using Blognet.Cms.Core.Model;
using Blognet.Cms.Domain.Entities;
using Blognet.Cms.Domain.Enum;
using Blognet.Cms.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blognet.Cms.Core.Albums.Queries.GetAlbumPhotosDetail;

namespace Blognet.Cms.Core.Services
{
    public class ArticleAlbumService : IArticleAlbumService
    {
        private IMediator _mediator;

        public ArticleAlbumService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<int> CreateAlbum(Article article)
        {
            if(article.Id == 0)
            {
                throw new EmptyIdentityKeyException("Article ID cannot be empty to create album.");
            }

            var resultId = await _mediator.Send(new CreateAlbumCommand
            {
                Name = $"{article.ProjectId}-article-{article.Id}",
                AlbumType = AlbumType.Article,
                Status = Status.Active

            });

            return resultId;
        }

        public async Task<Article> SaveArticleImages(Article storedArticle, ArticleImagesDTO articleImages)
        {
            if (articleImages.FileThumbnail != null)
            {
                await _mediator.Send(new UploadPhotosCommand {
                    AlbumId = storedArticle.AlbumId.Value,
                    LocalFiles = new List<IFormFile>() { articleImages.FileThumbnail },
                    PhotoType = PhotoType.Thumbnail
                });

                //storedArticle.PhotoThumbnailId = thumbnailList.Count > 0 ? (int?)thumbnailList[0].Id : null;
            }

            // Upload header
            if (articleImages.FileHeader != null)
            {
                await _mediator.Send(new UploadPhotosCommand
                {
                    AlbumId = storedArticle.AlbumId.Value,
                    LocalFiles = new List<IFormFile>() { articleImages.FileHeader },
                    PhotoType = PhotoType.Header
                });

                //storedArticle.PhotoHeaderId = headerList.Count > 0 ? (int?)headerList[0].Id : null;
            }

            // Upload others to album
            if (articleImages.Files != null && articleImages.Files.Count > 0)
            {
                await _mediator.Send(new UploadPhotosCommand
                {
                    AlbumId = storedArticle.AlbumId.Value,
                    LocalFiles = articleImages.Files,
                    PhotoType = PhotoType.Image
                });
            }

            // Download thumbnail
            if (articleImages.RemoteFileThumbnail != null && articleImages.FileThumbnail == null)
            {
                await _mediator.Send(new UploadPhotosCommand
                {
                    AlbumId = storedArticle.AlbumId.Value,
                    Files = new List<string>() { articleImages.RemoteFileThumbnail },
                    PhotoType = PhotoType.Thumbnail
                });

                //storedArticle.PhotoThumbnailId = thumbnailList.Count > 0 ? (int?)thumbnailList[0].Id : null;
            }

            // Download header
            if (articleImages.RemoteFileHeader != null && articleImages.FileHeader == null)
            {
                await _mediator.Send(new UploadPhotosCommand
                {
                    AlbumId = storedArticle.AlbumId.Value,
                    Files = new List<string>() { articleImages.RemoteFileHeader },
                    PhotoType = PhotoType.Header
                });

                //storedArticle.PhotoHeaderId = headerList.Count > 0 ? (int?)headerList[0].Id : null;
            }

            // Upload others to album
            var remoteFiles = articleImages.RemoteFiles != null ? articleImages.RemoteFiles.Split(';').ToList() : new List<string>();
            if (remoteFiles.Count > 0)
            {
                await _mediator.Send(new UploadPhotosCommand
                {
                    AlbumId = storedArticle.AlbumId.Value,
                    Files = remoteFiles,
                    PhotoType = PhotoType.Image
                });
            }

            var albumPhotos = await _mediator.Send(new GetAlbumPhotosDetailQuery {AlbumId = storedArticle.AlbumId.Value});
            var headerPhoto = albumPhotos.FirstOrDefault(x => x.Type == PhotoType.Header);
            var thumbnailPhoto = albumPhotos.FirstOrDefault(x => x.Type == PhotoType.Thumbnail);
            storedArticle.PhotoHeaderId = headerPhoto?.Id;
            storedArticle.PhotoThumbnailId = thumbnailPhoto?.Id;

            return storedArticle;
        }
    }
}
