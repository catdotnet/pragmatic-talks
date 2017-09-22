using System.Collections.Generic;

namespace PragmaticTalks.Data
{
    public interface IPagedEnumerable<TProjected>
    {
        IEnumerable<TProjected> Items { get; }
        int Page { get; }
        int PageSize { get; }
        int TotalPages { get; }
    }
}