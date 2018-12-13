using Core.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Entities
{
    [Table("cms_album")]
    public class Album : BaseEntity
    {
        //public int ProjectId { get; set; }
        //public Project Project { get; set; }
        
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        [MaxLength(255)]
        public string NameNormalized { get; set; }

        [Required]
        public AlbumType Type { get; set; }

        [Required]
        public Status Status { get; set; }

        public List<Photo> Photos { get; set; }

        public List<Article> Articles { get; set; }
    }
}
