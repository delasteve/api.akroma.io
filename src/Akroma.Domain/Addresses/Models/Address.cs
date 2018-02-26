using System.Collections.Generic;
using Akroma.Domain.Transactions.Models;

namespace Akroma.Domain.Addresses.Models
{
    public class Address
    {
        public Address(string hash, string balance, int mined, int totalTransactions, IEnumerable<Transaction> transactions)
        {
            Hash = hash;
            Balance = balance;
            Mined = mined;
            TotalTransactions = totalTransactions;
            Transactions = transactions;
        }

        public string Hash { get; }
        public string Balance { get; }
        public int Mined { get; }
        public int TotalTransactions { get; }
        public IEnumerable<Transaction> Transactions { get; }
    }
}
