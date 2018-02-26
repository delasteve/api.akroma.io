using System.Collections.Generic;
using Akroma.Domain.Prices.Models;
using Brickweave.Cqrs;

namespace Akroma.Domain.Prices.Queries
{
    public class GetPrice : IQuery<IEnumerable<Price>>
    {
        public GetPrice()
        {
        }
    }
}
