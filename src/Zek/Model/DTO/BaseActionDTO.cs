namespace Zek.Model.DTO
{
    public class BaseActionDTO<TId>
    {
        /// <summary>
        /// ID
        /// </summary>
        public TId Id { get; set; }

        /// <summary>
        /// Controller name
        /// </summary>
        public string Controller { get; set; }
    }
}