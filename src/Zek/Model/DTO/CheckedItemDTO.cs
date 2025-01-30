namespace Zek.Model.DTO
{
    [Obsolete]
    public class CheckedItemDTO : CheckedItemDTO<int>
    {
    }

    [Obsolete]
    public class CheckedItemDTO<TValue>
    {
        public TValue Value { get; set; } = default!;
        public string? Name { get; set; }
        public bool Checked { get; set; }

        public bool? Required { get; set; }
        public bool? ReadOnly { get; set; }
    }
}
