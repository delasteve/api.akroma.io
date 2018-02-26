using Akroma.Domain.Transactions.Models;

namespace Akroma.Persistence.SQL.Entities
{
    public class TransactionEntity : BaseEntity
    {
        public string Hash { get; set; }
        public string Nonce { get; set; }
        public string BlockHash { get; set; }
        public int TransactionIndex { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public decimal Value { get; set; }
        public string Gas { get; set; }
        public string GasPrice { get; set; }
        public long Timestamp { get; set; }
        public string Input { get; set; }

        public Transaction ToTransaction()
        {
            return new Transaction(
                Hash, Nonce, BlockHash, TransactionIndex, From, To, Value,
                Gas, GasPrice, Timestamp, Input
            );
        }
    }
}
