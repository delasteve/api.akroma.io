using System;
using System.Collections.Generic;
using Akroma.Domain.Transactions.Models;
using Brickweave.Cqrs;

namespace Akroma.Domain.Transactions.Queries
{
    public class GetTransactions : IQuery<IEnumerable<Transaction>>
    {
        public GetTransactions(int limit = 50)
        {
            Guard.AgainstInvalidLimitBounds(limit);

            Limit = limit;
        }

        public int Limit { get; }

        class Guard
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
