using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blognet.Cms.Core.Model
{
    public class SeoDTO
    {
        public int? Id { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }

        public string Keywords { get; set; }

        public bool IsEmpty
        {
            get
            {
                return !(Title != null || Description != null || Keywords != null);
            }
        }
    }
}
