namespace Zek.Model.DTO
{
    public class CheckedItemDTO : CheckedItemDTO<int>
    {
    }

    public class CheckedItemDTO<TId>
    {
        public TId Id { get; set; }
        public string Name { get; set; }
        public bool Checked { get; set; }
    }
}
