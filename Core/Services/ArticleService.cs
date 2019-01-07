using Core.Entities;
using Core.Enum;
using Core.Extensions;
using Core.Interfaces;
using Core.Interfaces.Repositories;
using Core.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Core.Services
{
    public class ArticleService : IArticleService
    {
        IArticleRepository _articleRepository;
        IPhotoRepository _photoRepository;
        ISeoService _seoService;
        IAlbumService _albumService;
        IPhotoService _photoService;
        IArticleSettingsService _articleSettingsService;

        public ArticleService(
            IArticleRepository articleRepository,
            IPhotoRepository photoRepository,
            ISeoService seoService,
            IAlbumService albumService,
            IPhotoService photoService,
            IArticleSettingsService articleSettingsService)
        {
            _articleRepository = articleRepository;
            _photoRepository = photoRepository;
            _seoService = seoService;
            _albumService = albumService;
            _photoService = photoService;
            _articleSettingsService = articleSettingsService;
        }

        public Task Create(Article article)
        {
            article.Status = ArticleStatus.Inactive;
            article.Slug = article.Title.GenerateSlug();

            // Do not store SEO with empty values
            if (article.Seo.IsEmpty())
            {
                article.Seo = null;
                article.SeoId = null;
            }

            //Create default article settings
            article.ArticleSettings = new ArticleSettings();

            return _articleRepository.Add(article);
        }

        public async Task Create(Article article, ArticleImagesDto articleImages)
        {
            await Create(article);

            // Create album
            var album = await CreateAlbum(article.ProjectId, article.Id);
            article.AlbumId = album.Id;

            await SaveImages(article, articleImages);
            await _articleRepository.Update(article);
            await ProcessArticleImageContent(article);
        }

        public Task Update(Article article)
        {
            //Update Seo data, if does not exist, create new entity.
            if (article.Seo.IsEmpty() && article.Seo.Id == 0)
            {
                article.Seo = null;
                article.SeoId = null;
            }

            return _articleRepository.Update(article);
        }

        public async Task Update(Article article, ArticleImagesDto articleImages)
        {
            await Update(article);

            // Create album if does not exist
            if (article.AlbumId == null)
            {
                var album = await CreateAlbum(article.ProjectId, article.Id);
                article.AlbumId = album.Id;
            }

            await SaveImages(article, articleImages);
            await _articleRepository.Update(article);
            await ProcessArticleImageContent(article);
        }

        public async Task ToggleStatusById(int id)
        {
            Article article = await _articleRepository.Get(id);
            article.Status = article.Status == ArticleStatus.Inactive ? ArticleStatus.Unpublished :
                (article.Status == ArticleStatus.Unpublished ? ArticleStatus.Published :
                (article.Status == ArticleStatus.Published ? ArticleStatus.Unpublished : ArticleStatus.Inactive));

            await _articleRepository.Update(article);
        }

        // @TODO Move to some CMS handle.
        private async Task ProcessArticleImageContent(Article article)
        {
            List<Photo> photos = article.AlbumId != null ? await _photoRepository.ListAllByAlbumId(article.AlbumId.Value) : new List<Photo>();

            // Get images from article album.
            var images = photos.Where(p => p.Type == PhotoType.Image).ToList();
            if(images.Count() == 0)
            {
                return;
            }
            
            // Find all <img> in content
            var imgSourceExpression = new Regex("<img[ a-zA-z\"'/=]*src=\"(?<ImageSource>[^\"]*)\"", RegexOptions.Multiline);
            var matches = imgSourceExpression.Matches(article.Content);
            int imageCount = 0;

            // Replace all img with relative path and unset data attribute matches in article content with album photo using default order. 
            foreach (Match match in matches)
            {
                string imagePath = match.Groups["ImageSource"].Value;

                // @TODO remove http: check in case of inserting CND or other path of stored album image
                if (!imagePath.StartsWith("http:") && !match.Value.Contains(@"data-cms-source=""album""") && images.Count() >= imageCount + 1)
                {
                    string imageTag = match.Value;
                    // Find same album image as linked one in content by source path
                    Photo existingImage = images.Where(i => i.GetImagePath() == imagePath).FirstOrDefault();
                    // Do not increment if actual replaced image is existing, this scenario could occurs when manually editing content source, unknown album local file for example
                    Photo actualImage = existingImage == null ? images[imageCount++] : images[imageCount];
                    string imageSource = existingImage == null ? actualImage.GetImagePath() : existingImage.GetImagePath();
                    int imageId = existingImage == null ? actualImage.Id : existingImage.Id;

                    imageTag = Regex.Replace(imageTag, @"src=""[^""]*""", @"src=""" + imageSource + @"""");
                    imageTag = Regex.Replace(imageTag, @"<img ", @"<img data-cms-source=""album"" data-cms-source-id=""" + imageId + @"""");
                    article.Content = article.Content.Replace(match.Value, imageTag);
                }   
            }

            await _articleRepository.Update(article);
        }

        private async Task<Album> CreateAlbum(int projectId, int articleId)
        {
            var album = new Album();
            album.Name = $"{projectId}-article-{articleId}";
            album.Status = Status.Active;

            await _albumService.Create(album, AlbumType.Article);
            return album;
        }

        /// <summary>
        /// Save images, store local files or download remote.
        /// Local files take precedence over remote files.
        /// </summary>
        /// <param name="storedArticle"></param>
        /// <param name="articleImages"></param>
        /// <returns></returns>
        private async Task<Article> SaveImages(Article storedArticle, ArticleImagesDto articleImages)
        {
            // Upload thumbnail
            if (articleImages.FileThumbnail != null)
            {
                var thumbnailList = await _photoService.UploadImages(storedArticle.AlbumId.Value, new List<IFormFile>() { articleImages.FileThumbnail }, PhotoType.Thumbnail);
                storedArticle.PhotoThumbnailId = thumbnailList.Count > 0 ? (int?)thumbnailList[0].Id : null;
            }

            // Upload header
            if (articleImages.FileHeader != null)
            {
                var headerList = await _photoService.UploadImages(storedArticle.AlbumId.Value, new List<IFormFile>() { articleImages.FileHeader }, PhotoType.Thumbnail);
                storedArticle.PhotoHeaderId = headerList.Count > 0 ? (int?)headerList[0].Id : null;
            }

            // Upload others to album
            if (articleImages.Files != null && articleImages.Files.Count > 0)
            {
                await _photoService.UploadImages(storedArticle.AlbumId.Value, articleImages.Files, PhotoType.Image);
            }

            // Download thumbnail
            if (articleImages.RemoteFileThumbnail != null && articleImages.FileThumbnail == null)
            {
                var thumbnailList = await _photoService.UploadImages(storedArticle.AlbumId.Value, new List<string>() { articleImages.RemoteFileThumbnail }, PhotoType.Thumbnail);
                storedArticle.PhotoThumbnailId = thumbnailList.Count > 0 ? (int?)thumbnailList[0].Id : null;
            }

            // Download header
            if (articleImages.RemoteFileHeader != null && articleImages.FileHeader == null)
            {
                var headerList = await _photoService.UploadImages(storedArticle.AlbumId.Value, new List<string>() { articleImages.RemoteFileHeader }, PhotoType.Thumbnail);
                storedArticle.PhotoHeaderId = headerList.Count > 0 ? (int?)headerList[0].Id : null;
            }

            // Upload others to album
            var remoteFiles = articleImages.RemoteFiles != null ? articleImages.RemoteFiles.Split(';').ToList() : new List<string>();
            if (remoteFiles.Count > 0)
            {
                await _photoService.UploadImages(storedArticle.AlbumId.Value, remoteFiles, PhotoType.Image);
            }

            return storedArticle;
        }
    }
}
