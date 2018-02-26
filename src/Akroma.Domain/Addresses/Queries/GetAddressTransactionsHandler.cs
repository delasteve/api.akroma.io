using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Akroma.Domain.Addresses.Models;
using Akroma.Domain.Blocks.Services;
using Akroma.Domain.Transactions.Models;
using Akroma.Domain.Transactions.Services;
using Brickweave.Cqrs;
using Nethereum.Web3;

namespace Akroma.Domain.Addresses.Queries
{
    public class GetAddressTransactionsHandler : IQueryHandler<GetAddressTransactions, AddressTransactions>
    {
        private readonly ITransactionsRepository _transactionsRepository;

        public GetAddressTransactionsHandler(ITransactionsRepository transactionsRepository)
        {
            _transactionsRepository = transactionsRepository;
        }

        public async Task<AddressTransactions> HandleAsync(GetAddressTransactions query)
        {
            var totalTransactionCountTask = _transactionsRepository.GetAddressTotalTransactionCount(query.Hash, query.TransactionType);
            var addressTransactionsTask = _transactionsRepository.GetTransactionsForAddress(query.Hash, query.Limit, query.Offset, query.TransactionType);

            await Task.WhenAll(totalTransactionCountTask, addressTransactionsTask);

            return new AddressTransactions(addressTransactionsTask.Result, totalTransactionCountTask.Result, query.Limit, query.Offset, query.TransactionType);
        }
    }
}
