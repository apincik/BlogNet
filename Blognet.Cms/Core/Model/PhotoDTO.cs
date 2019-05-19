using Blognet.Cms.Domain.Enum;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Blognet.Cms.Core.Model
{
    public class PhotoDTO
    {
        public int? Id { get; set; }

        public int? AlbumId { get; set; }

        public string Name { get; set; }

        public string NameNormalized { get; set; }

        public string Extension { get; set; }

        public string Hash { get; set; }

        public string ImageHash { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }
        public string Protocol { get; set; }

        public string DomainName { get; set; }

        public string RelativePath { get; set; }

        public string CdnPath { get; set; }

        public string OriginSource { get; set; }

        public Status Status { get; set; }

        public PhotoType Type { get; set; }

        public bool IsLocal { get; set; }

        public AlbumDTO Album { get; set; }

        public string ImagePath
        {
            get {
                if (IsLocal)
                {
                    return Path.Combine($"/{RelativePath}", AlbumId.ToString(), $"{Hash}.{Extension}").Replace(@"\", "/");
                }
                else
                {
                    return Path.Combine($"{Protocol}://{DomainName}/", RelativePath, AlbumId.ToString(), $"{Hash}.{Extension}").Replace(@"\", "/");
                }
            }
        }
    }
}
