using Akroma.Domain.Transactions.Models;
using Brickweave.Cqrs;

namespace Akroma.Domain.Transactions.Queries
{
    public class GetTransaction : IQuery<Transaction>
    {
        public GetTransaction(string hash)
        {
            Hash = hash;
        }

        public string Hash { get; }
    }
}
