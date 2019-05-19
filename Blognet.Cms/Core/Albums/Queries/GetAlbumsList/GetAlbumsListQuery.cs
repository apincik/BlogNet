using Blognet.Cms.Core.Albums.Models;
using Blognet.Cms.Core.Interfaces;
using Blognet.Cms.Core.Model;
using Domain.Enum;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blognet.Cms.Core.Albums.Queries.GetAlbumsList
{
    public class GetAlbumsListQuery : IRequest<List<AlbumDTO>>, IFilterRequestQuery
    {
        public int? Page { get; set; }
        public int? Limit { get; set; }
        public Order? Order { get; set; }
        public string OrderByColumnName { get; set; }
    }
}
