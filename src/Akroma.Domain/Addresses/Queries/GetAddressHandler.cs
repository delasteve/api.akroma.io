using System.Threading.Tasks;
using Akroma.Domain.Addresses.Models;
using Akroma.Domain.Blocks.Services;
using Akroma.Domain.Transactions.Models;
using Akroma.Domain.Transactions.Services;
using Brickweave.Cqrs;
using Nethereum.Web3;

namespace Akroma.Domain.Addresses.Queries
{
    public class GetAddressHandler : IQueryHandler<GetAddress, Address>
    {
        private readonly Nethereum.Web3.Web3 _web3;
        private readonly ITransactionsRepository _transactionsRepository;
        private readonly IBlocksRepository _blocksRepository;

        public GetAddressHandler(Nethereum.Web3.Web3 web3, ITransactionsRepository transactionsRepository, IBlocksRepository blocksRepository)
        {
            _web3 = web3;
            _transactionsRepository = transactionsRepository;
            _blocksRepository = blocksRepository;
        }

        public async Task<Address> HandleAsync(GetAddress query)
        {
            var balanceTask = _web3.Eth.GetBalance.SendRequestAsync(query.Hash);
            var blocksMinedTask = _blocksRepository.GetBlocksMinedForAddress(query.Hash);
            var totalTransactionCountTask = _transactionsRepository.GetAddressTotalTransactionCount(query.Hash, TransactionType.All);
            var addressLatestTransactionsTask = _transactionsRepository.GetTransactionsForAddress(query.Hash, 20, 0, TransactionType.All);

            await Task.WhenAll(balanceTask, blocksMinedTask, totalTransactionCountTask, addressLatestTransactionsTask);

            return new Address(
                query.Hash.ToLower(), balanceTask.Result.HexValue,
                blocksMinedTask.Result, totalTransactionCountTask.Result,
                addressLatestTransactionsTask.Result
            );
        }
    }
}
