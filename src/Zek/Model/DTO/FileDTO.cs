using Zek.Utils;

namespace Zek.Model.DTO
{
    [Obsolete]
    public class FileBaseDTO : EditBaseDTO
    {
        public string? Name { get; set; }
        public string? FileName { get; set; }

        public long FileSize { get; set; }
        public string? FileType { get; set; }
    }

    [Obsolete]

    public class DownloadFileDTO : FileBaseDTO
    {
        public string? Path { get; set; }
        public int? MimeTypeId { get; set; }
        public CompressionType? CompressionTypeId { get; set; }

    }
    [Obsolete]
    public class FileDTO : FileBaseDTO
    {
        public byte[]? Data { get; set; }
    }
    [Obsolete]
    public class FileStringDTO : FileBaseDTO
    {
        public string? Data { get; set; }
    }
}
