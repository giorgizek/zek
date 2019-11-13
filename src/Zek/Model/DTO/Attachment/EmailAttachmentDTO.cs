namespace Zek.Model.DTO.Attachment
{
    public class EmailAttachmentDTO : AttachmentBaseDTO
    {
        public byte[] FileData { get; set; }

        public string ContentId { get; set; }
    }
}