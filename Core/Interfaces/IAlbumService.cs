using Core.Entities;
using Core.Enum;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IAlbumService : IService<Album>
    {
        Task<Album> Create(Album album, AlbumType type = AlbumType.Custom);
        Task<Album> Update(Album album);

        Task ToggleStatusById(int id);
        
        Task<List<Album>> ListAllByIdWithPhotos();
    }
}
