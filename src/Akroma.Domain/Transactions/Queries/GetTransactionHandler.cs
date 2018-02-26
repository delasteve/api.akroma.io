using System.Threading.Tasks;
using Akroma.Domain.Transactions.Models;
using Akroma.Domain.Transactions.Services;
using Brickweave.Cqrs;

namespace Akroma.Domain.Transactions.Queries
{
    public class GetTransactionHandler : IQueryHandler<GetTransaction, Transaction>
    {
        private readonly ITransactionsRepository _transactionsRepository;

        public GetTransactionHandler(ITransactionsRepository transactionsRepository)
        {
            _transactionsRepository = transactionsRepository;
        }

        public async Task<Transaction> HandleAsync(GetTransaction query)
        {
            return await _transactionsRepository.GetTransactionAsync(query.Hash);
        }
    }
}
