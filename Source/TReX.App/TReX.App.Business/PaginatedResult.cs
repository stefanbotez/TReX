using System.Collections.Generic;

namespace TReX.App.Business
{
    public sealed class PaginatedResult<T>
    {
        public PaginatedResult(IEnumerable<T> items, int totalCount)
        {
            Items = items;
            TotalCount = totalCount;
        }

        public IEnumerable<T> Items { get; private set; }

        public int TotalCount { get; private set; }
    }
}