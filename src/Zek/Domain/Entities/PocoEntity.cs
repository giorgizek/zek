namespace Zek.Domain.Entities
{
    public class PocoEntity : PocoEntity<int> { }
    public class PocoEntity<TId> : CreateEntity<TId>
    {
        public int? ModifierId { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}