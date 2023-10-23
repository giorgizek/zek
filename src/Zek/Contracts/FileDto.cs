namespace Zek.Contracts
{
    public class FileDto<TId>
    {
        public TId Id { get; set; }
        public string FileName { get; set; }
        public int? FileSize { get; set; }
        public string Extension { get; set; }
        public string MediaType { get; set; }
        public byte[] Content { get; set; }
        public bool? IsDeleted { get; set; }
    }
    public class FileDto : FileDto<int?>
    {
    }
}
