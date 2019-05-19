using Blognet.Cms.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blognet.Cms.Core.Model
{
    public class AlbumDTO
    {
        public int? Id { get; set; }
        public string Name { get; set; }

        public string NameNormalized { get; set; }

        public AlbumType Type { get; set; }

        public Status Status { get; set; }

        public IEnumerable<PhotoDTO> Photos { get; set; }
    }
}
