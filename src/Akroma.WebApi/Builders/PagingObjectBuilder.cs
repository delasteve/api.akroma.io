using System.Collections.Generic;
using System.Linq;
using Akroma.WebApi.Models;

namespace Akroma.WebApi.Builders
{
    public class PagingObjectBuilder<T>
    {
        private string _baseRoute = "";
        private IEnumerable<T> _items = null;
        private int _total = 0;
        private int _limit = 0;
        private int _offset = 0;
        private IDictionary<string, string> _extraQueryParams = new Dictionary<string, string>();

        public PagingObjectBuilder<T> WithBaseRoute(string baseRoute)
        {
            _baseRoute = baseRoute;

            return this;
        }

        public PagingObjectBuilder<T> WithItems(IEnumerable<T> items)
        {
            _items = items;

            return this;
        }

        public PagingObjectBuilder<T> WithTotal(int total)
        {
            _total = total;

            return this;
        }

        public PagingObjectBuilder<T> WithLimit(int limit)
        {
            _limit = limit;

            return this;
        }

        public PagingObjectBuilder<T> WithOffset(int offset)
        {
            _offset = offset;

            return this;
        }

        public PagingObjectBuilder<T> WithExtraQueryParams(IDictionary<string, string> extraQueryParams)
        {
            _extraQueryParams = extraQueryParams;

            return this;
        }

        public PagingObject<T> Build()
        {
            var currentUrl = AppendExtraQueryParams($"{_baseRoute}?limit={_limit}&offset={_offset}");
            var previousUrl = AppendExtraQueryParams(GetPreviousPagedUrl(_baseRoute, _limit, _offset, _total));
            var nextUrl = AppendExtraQueryParams(GetNextPagedUrl(_baseRoute, _limit, _offset, _total));

            return new PagingObject<T>(_items, _total, _limit, _offset, currentUrl, previousUrl, nextUrl);
        }

        private string AppendExtraQueryParams(string url)
        {
            if (_extraQueryParams.Keys.Any() && url != null)
            {
                var extraQueryParams = string.Join("&", _extraQueryParams.Select(pair => $"{pair.Key}={pair.Value}"));
                url = $"{url}&{extraQueryParams}";
            }

            return url;
        }

        private string GetPreviousPagedUrl(string routePath, int limit, int offset, int total)
        {
            var hasOnlyOnePage = limit >= total;
            var isOnFirstPage = offset == 0 && total >= limit;

            if (hasOnlyOnePage || isOnFirstPage)
            {
                return null;
            }

            return $"{routePath}?limit={limit}&offset={offset - 1}";
        }

        private string GetNextPagedUrl(string routePath, int limit, int offset, int total)
        {
            var updatedOffset = offset + 1;
            var hasOnlyOnePage = limit >= total;
            var isOnLastPage = (updatedOffset * limit) >= total;

            if (hasOnlyOnePage || isOnLastPage)
            {
                return null;
            }

            return $"{routePath}?limit={limit}&offset={offset + 1}";
        }

    }
}
