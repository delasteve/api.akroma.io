using System.Collections.Generic;

namespace Akroma.WebApi.Models
{
    public class PagingObject<T>
    {
        public PagingObject(IEnumerable<T> items, int total, int limit, int offset, string href, string previous, string next)
        {
            Items = items;
            Total = total;
            Limit = limit;
            Offset = offset;
            Href = href;
            Previous = previous;
            Next = next;
        }

        public IEnumerable<T> Items { get; }
        public int Total { get; }
        public int Limit { get; }
        public int Offset { get; }
        public string Href { get; }
        public string Previous { get; }
        public string Next { get; }
    }
}
