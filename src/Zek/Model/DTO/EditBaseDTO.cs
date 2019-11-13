namespace Zek.Model.DTO
{
    public class EditBaseDTO
    {
        public int? Id { get; set; }

        public bool ReadOnly { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
