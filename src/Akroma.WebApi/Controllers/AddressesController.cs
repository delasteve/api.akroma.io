using System.Threading.Tasks;
using Akroma.Domain.Addressess.Model;
using Akroma.Domain.Addressess.Queries;
using Brickweave.Cqrs;
using Microsoft.AspNetCore.Mvc;

namespace Akroma.WebApi.Controllers
{
    public class AddressesController : Controller
    {
        private readonly IDispatcher _dispatcher;

        public AddressesController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        /// <summary>
        /// List transactions
        /// </summary>
        /// <param name="address">The address to return [0x]ADDRESS</param>
        /// <param name="page"></param>
        [ProducesResponseType(typeof(Address), 200)]
        [HttpGet, Route("addresses/{address}")]
        [ResponseCache(Duration = 30)]
        public async Task<Address> Get(string address, int page = 0)
        {
            return await _dispatcher.DispatchQueryAsync(new GetAddress(address, page));
        }

        /// <summary>
        /// List transactions
        /// </summary>
        /// <param name="address">The address to return [0x]ADDRESS</param>
        /// <param name="filter">all/to/from</param>
        /// <param name="page"></param>
        [ProducesResponseType(typeof(AddressTransactions), 200)]
        [HttpGet, Route("addresses/{address}/transactions")]
        public async Task<AddressTransactions> GetTransactions(string address, string filter = "all", int page = 0)
        {
            return await _dispatcher.DispatchQueryAsync(new GetAddressTransactions(address, filter, page));
        }
    }
}
