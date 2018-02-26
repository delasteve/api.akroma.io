using Akroma.Domain.Addressess.Model;
using Brickweave.Cqrs;

namespace Akroma.Domain.Addressess.Queries
{
    public class GetAddressTransactions : IQuery<AddressTransactions>
    {
        public GetAddressTransactions(string address, string filter, int page)
        {
            Address = address;
            Filter = filter;
            Page = page;
        }
        public string Address { get; }
        public string Filter { get; }
        public int Page { get; }
    }
}
