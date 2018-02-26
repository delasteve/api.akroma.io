using System.Threading.Tasks;
using Akroma.Domain.Addressess.Model;
using Akroma.Domain.Transactions.Services;
using Brickweave.Cqrs;

namespace Akroma.Domain.Addressess.Queries
{
    public class GetAddressTransactionsHandler : IQueryHandler<GetAddressTransactions, AddressTransactions>
    {
        private readonly ITransactionsRepository _repository;

        public GetAddressTransactionsHandler(ITransactionsRepository repository)
        {
            _repository = repository;
        }
        public async Task<AddressTransactions> HandleAsync(GetAddressTransactions query)
        {
            return await _repository.GetAddressTransactions(query.Address, query.Filter, query.Page);
        }
    }
}
