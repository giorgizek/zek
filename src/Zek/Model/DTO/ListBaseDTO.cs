namespace Zek.Model.DTO
{
    [Obsolete]
    public class ListBaseDTO : ListBaseDTO<int>
    {

    }
    [Obsolete]
    public class ListBaseDTO<T>
    {
        public T Id { get; set; } = default!;

        public bool? IsDeleted { get; set; }
    }
}