namespace Zek.Model.DTO.Attachment
{
    public class FileDataDTO
    {
        public int? Id { get; set; }

        public int? ApplicationId { get; set; }

        public int? AreaId { get; set; }

        public string FileName { get; set; }

        public byte[] FileData { get; set; }
    }
}
