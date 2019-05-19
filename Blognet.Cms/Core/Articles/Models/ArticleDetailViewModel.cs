using Blognet.Cms.Core.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blognet.Cms.Core.Articles.Models
{
    public class ArticleDetailViewModel
    {
        public ArticleDetailViewModel()
        {
            ArticleSettings = new ArticleSettingsDTO();
        }

        public int Id { get; set; }

        [Required]
        public int ProjectId { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public string Content { get; set; }

        public string Tags { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public CategoryDTO Category { get; set; }

        public SeoDTO Seo { get; set; }

        [Display(Name = "Thumbnail")]
        public IFormFile FileThumbnail { get; set; }

        [Display(Name = "Header")]
        public IFormFile FileHeader { get; set; }

        [Display(Name = "Images")]
        public List<IFormFile> Files { get; set; }

        [Display(Name = "Remote thumbnail")]
        public string RemoteFileThumbnail { get; set; }

        [Display(Name = "Remote header")]
        public string RemoteFileHeader { get; set; }

        [Display(Name = "Remote files (Delimiter = ';')")]
        public string RemoteFiles { get; set; }

        public List<CategoryDTO> Categories { get; set; }

        public ArticleSettingsDTO ArticleSettings { get; set; }

        public PhotoDTO PhotoThumbnail { get; set; }
        public PhotoDTO PhotoHeader { get; set; }

        public AlbumDTO Album { get; set; }
    }
}
