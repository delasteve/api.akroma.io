namespace Akroma.Persistence.SQL.Entities
{
    public class UncleEntity : BaseEntity
    {
        public BlockEntity Block { get; set; }
        public string Data { get; set; }
    }
}
