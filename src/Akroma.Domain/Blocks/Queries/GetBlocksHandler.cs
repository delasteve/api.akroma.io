using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Akroma.Domain.Blocks.Models;
using Akroma.Domain.Blocks.Services;
using Brickweave.Cqrs;

namespace Akroma.Domain.Blocks.Queries
{
    public class GetBlocksHandler : IQueryHandler<GetBlocks, IEnumerable<Block>>
    {
        private readonly IBlocksRepository _blocksRepository;

        public GetBlocksHandler(IBlocksRepository blocksRepository)
        {
            _blocksRepository = blocksRepository;
        }

        public async Task<IEnumerable<Block>> HandleAsync(GetBlocks query)
        {
            return await _blocksRepository.GetBlocksAsync(query.Limit);
        }
    }
}
