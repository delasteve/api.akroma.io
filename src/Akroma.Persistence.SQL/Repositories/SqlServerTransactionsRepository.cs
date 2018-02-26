using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Akroma.Domain.Transactions.Models;
using Akroma.Domain.Transactions.Services;
using Akroma.Persistence.SQL;
using Microsoft.EntityFrameworkCore;

namespace Akroma.Persistence.SQL.Repositories
{
    public class SqlServerTransactionsRepository : ITransactionsRepository
    {
        private readonly AkromaContext _context;

        public SqlServerTransactionsRepository(AkromaContext context)
        {
            _context = context;
        }

        public async Task<Transaction> GetTransactionAsync(string hash)
        {
            var transaction = await _context
                .Transactions
                .AsNoTracking()
                .SingleAsync(t => t.Hash == hash);

            return transaction.ToTransaction();
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsAsync(int limit)
        {
            return await _context
                .Transactions
                .AsNoTracking()
                .OrderByDescending(x => x.Timestamp)
                .Take(limit)
                .Select(x => x.ToTransaction())
                .ToListAsync();
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsForAddress(string address, int limit, int offset, TransactionType transactionType)
        {
            var query = _context
                .Transactions
                .AsNoTracking();

            if (transactionType == TransactionType.All)
            {
                query = query.Where(x => x.From.ToLower() == address.ToLower() || x.To.ToLower() == address.ToLower());
            }

            if (transactionType == TransactionType.To)
            {
                query = query.Where(x => x.To.ToLower() == address.ToLower());
            }

            if (transactionType == TransactionType.From)
            {
                query = query.Where(x => x.From.ToLower() == address.ToLower());
            }

            return await query
                .OrderByDescending(x => x.Timestamp)
                .Skip(offset * limit)
                .Take(limit)
                .Select(x => x.ToTransaction())
                .ToListAsync();
        }

        public Task<int> GetAddressTotalTransactionCount(string address, TransactionType type)
        {
            var query = _context
                .Transactions
                .AsNoTracking();

            if (type == TransactionType.All)
            {
                query = query.Where(x => x.From.ToLower() == address.ToLower() || x.To.ToLower() == address.ToLower());
            }

            if (type == TransactionType.To)
            {
                query = query.Where(x => x.To.ToLower() == address.ToLower());
            }

            if (type == TransactionType.From)
            {
                query = query.Where(x => x.From.ToLower() == address.ToLower());
            }

            return query.CountAsync();
        }
    }
}
