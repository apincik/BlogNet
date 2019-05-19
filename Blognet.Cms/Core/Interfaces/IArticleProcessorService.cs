using Blognet.Cms.Core.Model;
using Blognet.Cms.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blognet.Cms.Core.Interfaces
{
    public interface IArticleProcessorService
    {
        string ProcessArticleImageContent(string content, string projectDomainName, List<PhotoDTO> images);
    }
}
