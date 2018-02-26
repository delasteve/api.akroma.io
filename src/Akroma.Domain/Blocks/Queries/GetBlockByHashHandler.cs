using System.Threading.Tasks;
using Akroma.Domain.Blocks.Models;
using Akroma.Domain.Blocks.Services;
using Brickweave.Cqrs;

namespace Akroma.Domain.Blocks.Queries
{
    public class GetBlockByHashHandler : IQueryHandler<GetBlockByHash, Block>
    {
        private readonly IBlocksRepository _blocksRepository;

        public GetBlockByHashHandler(IBlocksRepository blocksRepository)
        {
            _blocksRepository = blocksRepository;
        }

        public async Task<Block> HandleAsync(GetBlockByHash query)
        {
            return await _blocksRepository.GetBlockByHashAsync(query.Hash);
        }
    }
}
