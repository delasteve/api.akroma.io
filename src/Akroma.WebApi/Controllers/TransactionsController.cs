using System.Collections.Generic;
using System.Threading.Tasks;
using Akroma.Domain.Transactions.Models;
using Akroma.Domain.Transactions.Queries;
using Akroma.WebApi.Models;
using Brickweave.Cqrs;
using Microsoft.AspNetCore.Mvc;

namespace Akroma.WebApi.Controllers
{
    [Produces("application/json")]
    public class TransactionsController : Controller
    {
        private readonly IDispatcher _dispatcher;

        public TransactionsController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        /// <summary>
        /// List transactions
        /// </summary>
        /// <param name="limit">The number of transactions to return (default: 50, min: 1, max: 100)</param>
        [ProducesResponseType(typeof(IEnumerable<Transaction>), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        [HttpGet, Route("transactions")]
        public async Task<IEnumerable<Transaction>> Get(int? limit = 50)
        {
            return await _dispatcher.DispatchQueryAsync(new GetTransactions(limit.Value));
        }

        /// <summary>
        /// Find transaction by hash
        /// </summary>
        /// <param name="hash">The transaction hash</param>
        [ProducesResponseType(typeof(Transaction), 200)]
        [ProducesResponseType(typeof(void), 404)]
        [HttpGet, Route("transactions/{hash}")]
        public async Task<Transaction> GetBlock(string hash)
        {
            return await _dispatcher.DispatchQueryAsync(new GetTransaction(hash));
        }
    }
}
