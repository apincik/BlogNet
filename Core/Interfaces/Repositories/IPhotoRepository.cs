using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Repositories
{
    public interface IPhotoRepository : IRepository<Photo>
    {
        Task<List<Photo>> ListAllByAlbumId(int albumId);
    }
}
