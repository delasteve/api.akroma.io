using System.Collections.Generic;
using System.Threading.Tasks;
using Akroma.Domain.Prices.Models;
using Akroma.Domain.Prices.Queries;
using Brickweave.Cqrs;
using Microsoft.AspNetCore.Mvc;

namespace Akroma.WebApi.Controllers
{
    [Produces("application/json")]
    public class PricesController
    {
        private readonly IDispatcher _dispatcher;

        public PricesController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        /// <summary>
        /// Get price of Akroma
        /// </summary>
        [ProducesResponseType(typeof(IEnumerable<Price>), 200)]
        [HttpGet, Route("prices")]
        [ResponseCache(Duration = 600)]
        public async Task<IEnumerable<Price>> Get()
        {
            return await _dispatcher.DispatchQueryAsync(new GetPrice());
        }

    }
}
