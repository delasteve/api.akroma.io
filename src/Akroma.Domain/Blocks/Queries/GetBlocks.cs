using System;
using System.Collections.Generic;
using Akroma.Domain.Blocks.Models;
using Brickweave.Cqrs;

namespace Akroma.Domain.Blocks.Queries
{
    public class GetBlocks : IQuery<IEnumerable<Block>>
    {
        public GetBlocks(int limit)
        {
            Guard.AgainstInvalidLimitBounds(limit);

            Limit = limit;
        }

        public int Limit { get; }

        private class Guard
        {
            public static void AgainstInvalidLimitBounds(int limit)
            {
                if (limit < 1 || limit > 100)
                {
                    throw new ArgumentException($"Invalid limit: {limit}. Limit must be between 1 and 100.");
                }
            }
        }
    }
}
