using Blognet.Cms.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blognet.Cms.Core.Interfaces.Repositories
{
    public interface IPhotoRepository : IRepository<Photo>
    {
        Task<List<Photo>> ListAllByAlbumId(int albumId);
    }
}
