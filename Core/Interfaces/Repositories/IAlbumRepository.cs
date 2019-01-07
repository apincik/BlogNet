using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Repositories
{
    public interface IAlbumRepository : IRepository<Album>
    {
        Task<List<Album>> ListAllByIdWithPhotos();
    }
}
