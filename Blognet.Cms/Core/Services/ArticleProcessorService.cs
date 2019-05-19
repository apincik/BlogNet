using Blognet.Cms.Core.Interfaces;
using Blognet.Cms.Core.Model;
using Blognet.Cms.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Blognet.Cms.Core.Services
{
    public class ArticleProcessorService : IArticleProcessorService
    {
        private IMediator _mediator;

        public ArticleProcessorService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public string ProcessArticleImageContent(string content, string projectDomainName, List<PhotoDTO> images)
        {
            //List<Photo> photos = article.AlbumId != null ? await _photoRepository.ListAllByAlbumId(article.AlbumId.Value) : new List<Photo>();

            // Get images from article album.
//            var images = photos.Where(p => p.Type == PhotoType.Image).ToList();
            if (images.Count() == 0)
            {
                return content;
            }

            // Find all <img> in content
            var imgSourceExpression = new Regex("<img[ a-zA-z\"'/=]*src=\"(?<ImageSource>[^\"]*)\"", RegexOptions.Multiline);
            var matches = imgSourceExpression.Matches(content);
            int imageCount = 0;

            //@TODO simple replace by image name

            // Replace all img with relative path and unset data attribute matches in article content with album photo using default order. 
            foreach (Match match in matches)
            {
                string imagePath = match.Groups["ImageSource"].Value;

                // @TODO remove http: check in case of inserting CDN or other path of stored album image
                // domain check because of - all CMS articles for projects should contain relative img path
                if (!imagePath.StartsWith("http:") || imagePath.Contains(projectDomainName) && !match.Value.Contains(@"data-cms-source=""album""") && images.Count() >= imageCount + 1)
                {
                    string imageTag = match.Value;
                    // Find same album image as linked one in content by source path
                    PhotoDTO existingImage = images.Where(i => i.ImagePath == imagePath).FirstOrDefault();
                    // Do not increment if actual replaced image is existing, this scenario could occurs when manually editing content source, unknown album local file for example
                    PhotoDTO actualImage = existingImage == null ? images[imageCount++] : images[imageCount];
                    string imageSource = existingImage == null ? actualImage.ImagePath : existingImage.ImagePath;
                    int imageId = existingImage == null ? actualImage.Id.Value : existingImage.Id.Value;

                    imageTag = Regex.Replace(imageTag, @"src=""[^""]*""", @"src=""" + imageSource + @"""");
                    imageTag = Regex.Replace(imageTag, @"<img ", @"<img data-cms-source=""album"" data-cms-source-id=""" + imageId + @"""");
                    content = content.Replace(match.Value, imageTag);
                }
            }

            return content;
        }
    }
}
