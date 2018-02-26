using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Akroma.Domain.Blocks.Models;
using Akroma.Domain.Blocks.Services;
using Akroma.Persistence.SQL;
using Microsoft.EntityFrameworkCore;

namespace Akroma.Persistence.SQL.Repositories
{
    public class SqlServerBlocksRepository : IBlocksRepository
    {
        private readonly AkromaContext _context;

        public SqlServerBlocksRepository(AkromaContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Block>> GetBlocksAsync(int limit)
        {
            return await _context
                .Blocks
                .AsNoTracking()
                .OrderByDescending(x => x.Number)
                .Take(limit)
                .Select(b => b.ToBlock())
                .ToListAsync();
        }

        public async Task<Block> GetBlockByHashAsync(string hash)
        {
            var block = await _context
                .Blocks
                .AsNoTracking()
                .SingleAsync(b => b.Hash == hash);

            return block.ToBlock();
        }

        public async Task<Block> GetBlockByNumberAsync(int number)
        {
            var block = await _context
                .Blocks
                .AsNoTracking()
                .SingleAsync(b => b.Number == number);

            return block.ToBlock();
        }

        public Task<int> GetBlocksMinedForAddress(string address)
        {
            return _context
                .Blocks
                .AsNoTracking()
                .CountAsync(b => b.Miner.ToLower() == address.ToLower());
        }
    }
}
