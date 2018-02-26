using System.Collections.Generic;
using Akroma.Domain.Transactions.Models;

namespace Akroma.Domain.Addresses.Models
{
    public class AddressTransactions
    {
        public AddressTransactions(IEnumerable<Transaction> transactions, int totalTransactions, int limit, int offset, TransactionType transactionType)
        {
            Transactions = transactions;
            Total = totalTransactions;
            Limit = limit;
            Offset = offset;
            TransactionType = transactionType;
        }

        public IEnumerable<Transaction> Transactions { get; }
        public int Total { get; }
        public int Limit { get; }
        public int Offset { get; }
        public TransactionType TransactionType { get; }
    }
}
