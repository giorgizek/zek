namespace Zek.Model.DTO.Form
{
    public class FormDTO : EditBaseDTO
    {
        public List<FieldDTO> Fields { get; set; }
    }


    public class FormDataDTO : EditBaseDTO
    {
        public List<FieldValueDTO> Fields { get; set; }
    }


    public class FieldValueDTO
    {
        public int Id { get; set; }
        public int FieldId { get; set; }
        public int RowId { get; set; }
        public string Value { get; set; }
    }
}
