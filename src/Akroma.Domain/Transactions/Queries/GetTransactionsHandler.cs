using System.Collections.Generic;
using System.Threading.Tasks;
using Akroma.Domain.Transactions.Models;
using Akroma.Domain.Transactions.Services;
using Brickweave.Cqrs;

namespace Akroma.Domain.Transactions.Queries
{
    public class GetTransactionsHandler : IQueryHandler<GetTransactions, IEnumerable<Transaction>>
    {
        private readonly ITransactionsRepository _transactionsRepository;

        public GetTransactionsHandler(ITransactionsRepository transactionRepository)
        {
            _transactionsRepository = transactionRepository;
        }

        public async Task<IEnumerable<Transaction>> HandleAsync(GetTransactions query)
        {
            return await _transactionsRepository.GetTransactionsAsync(query.Limit);
        }
    }
}
