using Akroma.Domain.Addresses.Models;
using Brickweave.Cqrs;

namespace Akroma.Domain.Addresses.Queries
{
    public class GetAddress : IQuery<Address>
    {
        public GetAddress(string hash)
        {
            Hash = hash;
        }

        public string Hash { get; }
    }
}
