namespace Zek.Model.DTO.Attachment
{
    [Obsolete]
    public class EmailAttachmentDTO : FileBaseDTO
    {
        public byte[] FileData { get; set; }

        public string ContentId { get; set; }
    }
}