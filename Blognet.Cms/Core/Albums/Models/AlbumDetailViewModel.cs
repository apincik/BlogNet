using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Blognet.Cms.Core.Albums.Models
{
    public class AlbumDetailViewModel
    {
        public int? Id;

        [Required]
        public string Name { get; set; }
    }
}
