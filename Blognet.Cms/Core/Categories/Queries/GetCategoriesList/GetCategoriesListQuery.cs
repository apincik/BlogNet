using Blognet.Cms.Core.Interfaces;
using Blognet.Cms.Core.Model;
using Domain.Enum;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blognet.Cms.Core.Categories.Queries.GetCategoriesList
{
    public class GetCategoriesListQuery : IRequest<List<CategoryDTO>>, IProjectRequestQuery, IFilterRequestQuery
    {
        public int ProjectId { get; set; }
        public int? Page { get; set; }
        public int? Limit { get; set; }
        public Order? Order { get; set; }
        public string OrderByColumnName { get; set; }
    }
}
