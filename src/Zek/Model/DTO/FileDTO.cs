using Zek.Utils;

namespace Zek.Model.DTO
{
    public class FileBaseDTO : EditBaseDTO
    {
        public string FileName { get; set; }

        public long FileSize { get; set; }
        public string FileType { get; set; }
    }

    public class DownloadFileDTO : FileBaseDTO
    {
        public string Path { get; set; }
        public int? MimeTypeId { get; set; }

        public CompressionType? CompressionTypeId { get; set; }

    }

    public class FileDTO : FileBaseDTO
    {
        public byte[] Data { get; set; }
    }
    public class FileStringDTO : FileBaseDTO
    {
        public string Data { get; set; }
    }
}
