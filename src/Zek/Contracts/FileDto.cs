namespace Zek.Contracts
{
    public class FileDto<TId, TContent>
    {
        public TId Id { get; set; }
        public string FileName { get; set; }
        public int? FileSize { get; set; }
        public string Extension { get; set; }
        public string MediaType { get; set; }
        public TContent Content { get; set; }
        public bool? IsDeleted { get; set; }
    }
    public class FileDto<TId> : FileDto<TId, byte[]>
    {
    }
    public class FileDto : FileDto<int?>
    {
    }
    public class Base64StringFileDto : FileDto<int?, string>
    {
    }
}
