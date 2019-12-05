namespace Zek.Model.DTO.Attachment
{
    public class EmailAttachmentDTO : FileBaseDTO
    {
        public byte[] FileData { get; set; }

        public string ContentId { get; set; }
    }
}