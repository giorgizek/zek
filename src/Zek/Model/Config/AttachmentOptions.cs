namespace Zek.Model.Config
{
    public class AttachmentOptions
    {
        public string Directory { get; set; }
        public long MaxFileSize { get; set; } = 50_000_000;
        public string[] AllowedExtensions { get; set; }
    }
}
