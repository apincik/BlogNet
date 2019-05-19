using System;
using System.Collections.Generic;
using System.Text;
using Blognet.Cms.Domain.Enum;

namespace Blognet.Cms.Core.Model
{
    public class PageForwardDTO
    {
        public int Id { get; set; }
        public string Mask { get; set; }

        public string DestinationUrl { get; set; }

        public string SourceId { get; set; }
        public PageForwardSourceType Type { get; set; }

        public int ProjectId { get; set; }
    }
}
