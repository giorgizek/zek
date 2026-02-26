namespace Zek.Contracts
{
    public class NodeDto<TId>
    {
        public TId Id { get; set; } = default!;
        public string? Name { get; set; }
        public int? ParentId { get; set; }
        public bool? Checked { get; set; }
    }
    public class NodeDto : NodeDto<int>
    {
    }
}
