using System;
using System.Collections.Generic;
using Akroma.Domain.Addresses.Models;
using Akroma.Domain.Transactions.Models;
using Brickweave.Cqrs;

namespace Akroma.Domain.Addresses.Queries
{
    public class GetAddressTransactions : IQuery<AddressTransactions>
    {
        public GetAddressTransactions(string hash, int limit, int offset, TransactionType transactionType)
        {
            Guard.NotNull("hash", hash);
            Guard.AgainstInvalidLimitBounds(limit);
            Guard.AgainstInvalidOffset(offset);

            Hash = hash;
            Limit = limit;
            Offset = offset;
            TransactionType = transactionType;
        }

        public string Hash { get; }
        public int Limit { get; }
        public int Offset { get; }
        public TransactionType TransactionType { get; }

        class Guard
        {
            public static void NotNull(string paramName, string param)
            {
                if (string.IsNullOrEmpty(param))
                {
                    throw new ArgumentNullException($"Invalid limit: {paramName}. {paramName} must not be null.");
                }
            }

            public static void AgainstInvalidLimitBounds(int limit)
            {
                if (limit < 1 || limit > 100)
                {
                    throw new ArgumentException($"Invalid limit: {limit}. Limit must be between 1 and 100.");
                }
            }

            public static void AgainstInvalidOffset(int offset)
            {
                if (offset < 0)
                {
                    throw new ArgumentException($"Invalid offset: {offset}. Offset must be a positive number.");
                }
            }
        }
    }
}
