using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blognet.Cms.Core.Interfaces
{
    public interface IFilterRequestQuery
    {
        int? Page { get; set; }
        int? Limit { get; set; }
        Order? Order { get; set; }
        string OrderByColumnName { get; set; }
    }
}
