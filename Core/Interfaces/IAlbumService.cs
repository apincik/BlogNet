using Core.Entities;
using Core.Enum;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IAlbumService
    {
        Task Create(Album album, AlbumType type = AlbumType.Custom);
        Task Update(Album album);
        Task ToggleStatusById(int id);
    }
}
