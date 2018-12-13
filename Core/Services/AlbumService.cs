using Core.Entities;
using Core.Enum;
using Core.Extensions;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class AlbumService : Service<Album>, IAlbumService
    {
        public AlbumService(IAsyncModel<Album> model) : base(model)
        {
        }

        public async Task<Album> Create(Album album, AlbumType type = AlbumType.Custom)
        {
            album.NameNormalized = album.Name.GenerateSlug();
            album.Status = Status.Inactive;
            album.Type = type;
            album = await Repository.AddAsync(album);

            return album;
        }

        public async Task<Album> Update(Album album)
        {
            album.NameNormalized = album.Name.GenerateSlug();
            await Repository.UpdateAsync(album);
            
            return album;
        }

        public Task<List<Album>> ListAllByIdWithPhotos()
        {
            throw new NotImplementedException();
        }

        public async Task ToggleStatusById(int id)
        {
            Album cat = await Repository.Table().FindAsync(id);
            cat.Status = cat.Status == Status.Inactive ? Status.Active : Status.Inactive;
            await Repository.UpdateAsync(cat);
        }
    }
}
