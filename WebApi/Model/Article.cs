using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Model
{
    /// <summary>
    /// DTO
    /// </summary>
    public class Article
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string PhotoThumbnailImagePath { get; set; }
        public string PhotoHeaderImagePath { get; set; }
        public string Slug { get; set; }
        public string CategoryTitle { get; set; }
        public Seo Seo { get; set; }
    }
}
