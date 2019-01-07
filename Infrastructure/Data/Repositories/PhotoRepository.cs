using Core.Entities;
using Core.Interfaces;
using Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class PhotoRepository : Repository<Photo>, IPhotoRepository
    {
        public PhotoRepository(WebContext dbContext) : base(dbContext)
        {
        }

        public Task<List<Photo>> ListAllByAlbumId(int albumId)
        {
            return _dbContext.Photos
                .Where(p => p.AlbumId == albumId)
                .ToListAsync();
        }
    }
}
