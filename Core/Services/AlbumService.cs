using Core.Entities;
using Core.Enum;
using Core.Extensions;
using Core.Interfaces;
using Core.Interfaces.Repositories;
using System.Threading.Tasks;

namespace Core.Services
{
    public class AlbumService : IAlbumService
    {
        private IAlbumRepository _albumRepository;

        public AlbumService(IAlbumRepository albumRepository)
        {
            _albumRepository = albumRepository;
        }

        public Task Create(Album album, AlbumType type = AlbumType.Custom)
        {
            album.NameNormalized = album.Name.GenerateSlug();
            album.Status = Status.Inactive;
            album.Type = type;
            return _albumRepository.Add(album);
        }

        public Task Update(Album album)
        {
            album.NameNormalized = album.Name.GenerateSlug();
            return _albumRepository.Update(album);
        }

        public async Task ToggleStatusById(int id)
        {
            Album cat = await _albumRepository.Get(id);
            cat.Status = cat.Status == Status.Inactive ? Status.Active : Status.Inactive;
            await _albumRepository.Update(cat);
        }
    }
}
