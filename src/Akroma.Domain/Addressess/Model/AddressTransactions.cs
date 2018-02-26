using System.Collections.Generic;
using Akroma.Domain.Transactions.Models;

namespace Akroma.Domain.Addressess.Model
{
    public class AddressTransactions
    {
        public AddressTransactions(IEnumerable<Transaction> transactions, int totalPages, int currentPage, int totalTransactions)
        {
            Transactions = transactions;
            TotalPages = totalPages;
            CurrentPage = currentPage;
            TotalTransactions = totalTransactions;
        }

        public int TotalPages { get; }
        public int CurrentPage { get; }
        public int TotalTransactions { get; }
        public IEnumerable<Transaction> Transactions { get; }
    }
}