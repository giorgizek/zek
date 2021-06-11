namespace Zek.Model.DTO
{
    public class ListBaseDTO : ListBaseDTO<int>
    {

    }

    public class ListBaseDTO<T>
    {
        public T Id { get; set; }

        public bool? IsDeleted { get; set; }
    }
}