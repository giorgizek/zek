namespace Zek.Model.DTO
{
    public class CheckedItemDTO : CheckedItemDTO<int>
    {
    }

    public class CheckedItemDTO<TValue>
    {
        public TValue Value { get; set; }
        public string Name { get; set; }
        public bool Checked { get; set; }

        public bool? Required { get; set; }
        public bool? ReadOnly { get; set; }
    }
}
