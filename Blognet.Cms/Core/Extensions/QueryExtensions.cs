using Blognet.Cms.Core.Interfaces;
using Domain.Enum;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blognet.Cms.Core.Extensions
{
    /// <summary>
    /// IQueryable extension methods.
    /// </summary>
    public static class QueryExtensions
    {
        /// <summary>
        /// Base query filter, ordering by ASC, DESC.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static IQueryable<T> OrderQuery<T>(this IQueryable<T> query, IFilterRequestQuery filter)
        {
            if (filter.Order != null && !String.IsNullOrEmpty(filter.OrderByColumnName))
                if (filter.Order == Order.ASC)
                    query = query.OrderBy(p => EF.Property<object>(p, filter.OrderByColumnName));
                else
                    query = query.OrderByDescending(p => EF.Property<object>(p, filter.OrderByColumnName));

            return query;
        }

        /// <summary>
        /// Limit and offset query.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static IQueryable<T> LimitQuery<T>(this IQueryable<T> query, IFilterRequestQuery filter)
        {
            if (filter.Limit != null && filter.Page != null)
                query = query.Skip(filter.Limit.Value * filter.Page.Value)
                    .Take(filter.Limit.Value);

            return query;
        }
    }
}
