using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Model
{
    public class ImageFileDto
    {
        public int Id { get; set; }

        public string Hash { get; set; }

        public string BasePath { get; set; }
        public string RelativePath { get; set; }
        public string Name { get; set; }
        public string Extension { get; set; }

        public int Width { get; set; }
        public int Height { get; set; }

        public bool IsLocal { get; set; }

        public string OriginSource { get; set; }
    }
}
