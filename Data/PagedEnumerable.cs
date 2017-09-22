using System;
using System.Collections.Generic;

namespace PragmaticTalks.Data
{
    internal class PagedEnumerable<TProjected> : IPagedEnumerable<TProjected>
    {
        public PagedEnumerable(int page, int pageSize, int total, IEnumerable<TProjected> items)
        {
            this.Page = page;
            this.PageSize = pageSize;
            this.TotalPages = (int)Math.Ceiling((double)total / pageSize);
            this.Items = items;
        }

        public IEnumerable<TProjected> Items { get; private set; }

        public int Page { get; private set; }

        public int PageSize { get; private set; }

        public int TotalPages { get; private set; }
    }
}
