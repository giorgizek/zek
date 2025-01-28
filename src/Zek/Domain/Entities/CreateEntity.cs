namespace Zek.Domain.Entities
{
    public class CreateEntity : CreateEntity<int> { }
    public class CreateEntity<TId>
    {
        public TId Id { get; set; } = default!;

        public bool IsDeleted { get; set; }

        public int CreatorId { get; set; }
        public DateTime CreateDate { get; set; }
    }
}