using System.Collections.Generic;
using System.Threading.Tasks;
using Akroma.Domain.Blocks.Models;
using Akroma.Domain.Blocks.Queries;
using Akroma.WebApi.Models;
using Brickweave.Cqrs;
using Microsoft.AspNetCore.Mvc;

namespace Akroma.WebApi.Controllers
{
    [Produces("application/json")]
    public class BlocksController : Controller
    {
        private readonly IDispatcher _dispatcher;

        public BlocksController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        /// <summary>
        /// List blocks
        /// </summary>
        /// <param name="limit">The number of blocks to return (default: 50, min: 1, max: 100)</param>
        [ProducesResponseType(typeof(IEnumerable<Block>), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        [HttpGet, Route("blocks")]
        public async Task<IEnumerable<Block>> Get(int? limit = 50)
        {
            return await _dispatcher.DispatchQueryAsync(new GetBlocks(limit.Value));
        }

        /// <summary>
        /// Find block by number
        /// </summary>
        /// <param name="number">The block number</param>
        [ProducesResponseType(typeof(Block), 200)]
        [ProducesResponseType(typeof(void), 404)]
        [HttpGet, Route("blocks/{number:int}")]
        public async Task<Block> GetBlock(int number)
        {
            return await _dispatcher.DispatchQueryAsync(new GetBlockByNumber(number));
        }

        /// <summary>
        /// Find block by hash
        /// </summary>
        /// <param name="hash">The block hash</param>
        [ProducesResponseType(typeof(Block), 200)]
        [ProducesResponseType(typeof(void), 404)]
        [HttpGet, Route("blocks/{hash}")]
        public async Task<Block> GetBlock(string hash)
        {
            return await _dispatcher.DispatchQueryAsync(new GetBlockByHash(hash));
        }
    }
}
