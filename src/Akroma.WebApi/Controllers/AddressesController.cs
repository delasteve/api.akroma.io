using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Akroma.Domain.Addresses.Models;
using Akroma.Domain.Addresses.Queries;
using Akroma.Domain.Transactions.Models;
using Akroma.WebApi.Builders;
using Akroma.WebApi.Models;
using Brickweave.Cqrs;
using Microsoft.AspNetCore.Mvc;

namespace Akroma.WebApi.Controllers
{
    [Produces("application/json")]
    public class AddressesController : Controller
    {
        private readonly IDispatcher _dispatcher;

        public AddressesController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        /// <summary>
        /// Find address by hash
        /// </summary>
        /// <param name="hash">The address hash</param>
        [ProducesResponseType(typeof(Address), 200)]
        [ProducesResponseType(typeof(void), 404)]
        [HttpGet, Route("addresses/{hash}")]
        public async Task<Address> GetAddress(string hash)
        {
            return await _dispatcher.DispatchQueryAsync(new GetAddress(hash));
        }

        /// <summary>
        /// Get transactions for address
        /// </summary>
        /// <param name="hash">The address hash</param>
        /// <param name="limit">The number of transactions to return (default: 50, min: 1, max: 100)</param>
        /// <param name="offset">The page offset of tranactions (default: 0)</param>
        /// <param name="type">The type of transactions to return (options: all | to | from) (default: all)</param>
        [ProducesResponseType(typeof(PagingObject<Transaction>), 200)]
        [ProducesResponseType(typeof(void), 404)]
        [HttpGet, Route("addresses/{hash}/transactions")]
        public async Task<PagingObject<Transaction>> GetTransactions(string hash, TransactionType? type, int? limit = 50, int? offset = 0)
        {
            var transactionType = type ?? TransactionType.All;

            var addressTransactions = await _dispatcher.DispatchQueryAsync(new GetAddressTransactions(hash, limit.Value, offset.Value, transactionType));

            var camelCaseTransactionType = $"{Char.ToLowerInvariant(addressTransactions.TransactionType.ToString()[0])}{addressTransactions.TransactionType.ToString().Substring(1)}";

            return new PagingObjectBuilder<Transaction>()
                .WithBaseRoute(Request.Path.Value)
                .WithItems(addressTransactions.Transactions)
                .WithTotal(addressTransactions.Total)
                .WithLimit(addressTransactions.Limit)
                .WithOffset(addressTransactions.Offset)
                .WithExtraQueryParams(new Dictionary<string, string> { { "type", camelCaseTransactionType } })
                .Build();
        }
    }
}
