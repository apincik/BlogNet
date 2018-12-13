using Core.Entities;
using Core.Enum;
using Core.Extensions;
using Core.Interfaces;
using Core.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class ArticleService : Service<Article>, IArticleService
    {
        ISeoService _seoService;
        IAlbumService _albumService;
        IPhotoService _photoService;
        IArticleSettingsService _articleSettingsService;

        public ArticleService(
            IAsyncModel<Article> model, 
            ISeoService seoService,
            IAlbumService albumService,
            IPhotoService photoService,
            IArticleSettingsService articleSettingsService) : base(model)
        {
            _seoService = seoService;
            _albumService = albumService;
            _photoService = photoService;
            _articleSettingsService = articleSettingsService;
        }

        public async Task<Article> Create(Article article)
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

            // Store article
            article = await Repository.AddAsync(article);

            // Create article settings
//          articleSettings.ArticleId = article.Id;
//          await _articleSettingsService.Create(articleSettings);

            return article;
        }

        public async Task<Article> Create(Article article, ArticleImagesDto articleImages)
        {
            var storedArticle = await Create(article);

            // Create album
            var album = await CreateAlbum(storedArticle.ProjectId, storedArticle.Id);
            storedArticle.AlbumId = album.Id;

            await SaveImages(storedArticle, articleImages);
            await Repository.UpdateAsync(storedArticle);

            return article;
        }

        public async Task<Article> Update(Article article)
        {
            //Update Seo data, if does not exist, create new entity.
            if (article.Seo.IsEmpty() && article.Seo.Id == 0)
            {
                article.Seo = null;
                article.SeoId = null;
            }

            await Repository.UpdateAsync(article);
            return article;
        }

        public async Task<Article> Update(Article article, ArticleImagesDto articleImages)
        {
            var storedArticle = await Update(article);

            // Create album if does not exist
            if (storedArticle.AlbumId == null)
            {
                var album = await CreateAlbum(storedArticle.ProjectId, storedArticle.Id);
                storedArticle.AlbumId = album.Id;
            }

            await SaveImages(storedArticle, articleImages);

            await Repository.UpdateAsync(storedArticle);
            return storedArticle;
        }

        public async Task ToggleStatusById(int id)
        {
            Article article = await Repository.Table().FindAsync(id);
            article.Status = article.Status == ArticleStatus.Inactive ? ArticleStatus.Unpublished :
                (article.Status == ArticleStatus.Unpublished ? ArticleStatus.Published :
                (article.Status == ArticleStatus.Published ? ArticleStatus.Unpublished : ArticleStatus.Inactive));

            await Repository.UpdateAsync(article);
        }

        private Task<Album> CreateAlbum(int projectId, int articleId)
        {
            var album = new Album();
            album.Name = $"{projectId}-article-{articleId}";

            return _albumService.Create(album, AlbumType.Article);
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
            if (articleImages.Files.Count > 0)
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

        #region Listing

        public override Task<List<Article>> ListAll()
        {
            return Repository.Table()
                .Include(m => m.Category)
                .Include(m => m.Seo)
                .ToListAsync();
        }

        public Task<List<Article>> ListAllByProjectId(int projectId)
        {
            return Repository.Table()
                .Include(m => m.Category)
                .Include(m => m.Seo)
                .Where(m => m.ProjectId == projectId)
                .ToListAsync();
        }

        public Task<Article> Get(int id)
        {
            return Repository.Table()
                //.AsNoTracking()
                .Include(a => a.Category)
                .Include(a => a.Seo)
                .Include(a => a.PhotoHeader)
                .Include(a => a.PhotoThumbnail)
                .Include(a => a.Album)
                    .ThenInclude(album => album.Photos)
                .Include(a => a.ArticleSettings)
                    .AsNoTracking()
                .Where(a => a.Id == id)
                .FirstAsync();
        }

        #endregion
    }
}
