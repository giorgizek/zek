namespace Zek.Office
{
    public class EmailAttachment
    {
        public string? FileName { get; set; }
        public byte[]? FileData { get; set; }

        public string? ContentId { get; set; }
    }
}