using System.Collections.Generic;
using System.Threading.Tasks;
using Akroma.Domain.Addressess.Model;
using Akroma.Domain.Transactions.Models;

namespace Akroma.Domain.Transactions.Services
{
    public interface ITransactionsRepository
    {
        Task<IEnumerable<Transaction>> GetTransactionsAsync(int limit);
        Task<Transaction> GetTransactionAsync(string hash);
        Task<AddressTransactions> GetAddressTransactions(string address, string filter, int currentPage);
    }
}
