using System;
using System.Collections.Generic;
using System.Linq;

namespace SBEISK.SGM.Domain.Queries
{
    public class FilteredQueryResult<T>
    {
        public FilteredQueryResult(IList<T> items, int total)
        {
            Items = items;
            Total = total;
        }

        public IList<T> Items { get; set; }
        public int Total { get; set; }

        public FilteredQueryResult<TDestiny> Transform<TDestiny>(Func<T, TDestiny> transform)
        {
            return new FilteredQueryResult<TDestiny>(this.Items.Select(transform).ToList(), this.Total);
        }
    }
}
