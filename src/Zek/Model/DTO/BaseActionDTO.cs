namespace Zek.Model.DTO
{
    [Obsolete]
    public class BaseActionDTO<TId>
    {
        /// <summary>
        /// ID
        /// </summary>
        public TId Id { get; set; } = default!;

        /// <summary>
        /// Controller name
        /// </summary>
        public string? Controller { get; set; }
    }
}