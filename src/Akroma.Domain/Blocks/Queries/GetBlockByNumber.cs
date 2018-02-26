using Akroma.Domain.Blocks.Models;
using Brickweave.Cqrs;

namespace Akroma.Domain.Blocks.Queries
{
    public class GetBlockByNumber : IQuery<Block>
    {
        public GetBlockByNumber(int number)
        {
            Number = number;
        }

        public int Number { get; }
    }
}
